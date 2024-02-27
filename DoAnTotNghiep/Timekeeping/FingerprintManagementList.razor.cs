using AntDesign;
using AntDesign.TableModels;
using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using DoAnTotNghiep.ViewModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Timekeeping
{
    public partial class FingerprintManagementList : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Inject] FingerprintManagementService FingerprintManagementService { get; set; }
        FingerprintManagementFilterEditModel fingerprintManagementFilterModel = new FingerprintManagementFilterEditModel();
        FingerprintManagementFilterEditModel excelFilterModel = new FingerprintManagementFilterEditModel();
        UsersSearch usersSearch = new UsersSearch();
        List<Users> ListUsers = new List<Users>();
        List<UsersViewModel> UsersViewModels = new List<UsersViewModel>();
        List<FingerprintGroup> FingerprintGroups = new List<FingerprintGroup>();
        List<FingerprintGroupViewModel> FingerprintGroupViewModels = new List<FingerprintGroupViewModel>();
        List<FingerprintManagement> FingerprintManagementExcelModels = new List<FingerprintManagement>();
        List<string> notExistCodes = new List<string>();
        bool usersLoading;
        bool loading;
        string errorMessage = "";
        bool confirmLoading = false;
        bool importVisible = false;
        string fileNameShow = "";
        string fileNameProcess = "";
        bool excelExporting;
        protected override async Task OnInitializedAsync()
        {
            try
            {
                usersSearch.Page = new Page() { PageIndex = 1, PageSize = 15 };
                fingerprintManagementFilterModel.Page = new Page() { PageIndex = 1, PageSize = 31 };
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task LoadUsersAsync()
        {
            try
            {
                usersLoading = true;
                StateHasChanged();
                var page = await UsersService.GetPageWithFilterAsync(usersSearch);
                ListUsers = page.Item1 ?? new List<Users>();
                UsersViewModels = Mapper.Map<List<UsersViewModel>>(ListUsers ?? new List<Users>());
                int stt = usersSearch.Page.PageSize * (usersSearch.Page.PageIndex - 1) + 1;
                UsersViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                usersSearch.Page.Total = page.Item2;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                usersLoading = false;
                StateHasChanged();
            }
        }

        async Task LoadDataAsync()
        {
            try
            {
                loading = true;
                StateHasChanged();
                var search = Mapper.Map<FingerprintManagementSearch>(fingerprintManagementFilterModel);
                var page = await FingerprintManagementService.GetPageWithFilterAsync(search);
                FingerprintGroups = page.Item1 ?? new List<FingerprintGroup>();
                FingerprintGroupViewModels = Mapper.Map<List<FingerprintGroupViewModel>>(FingerprintGroups ?? new List<FingerprintGroup>());
                int stt = fingerprintManagementFilterModel.Page.PageSize * (fingerprintManagementFilterModel.Page.PageIndex - 1) + 1;
                FingerprintGroupViewModels.ForEach(c =>
                {
                    c.Stt = stt++;
                });
                fingerprintManagementFilterModel.Page.Total = page.Item2;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                loading = false;
                StateHasChanged();
            }
        }

        async Task SearchAsync()
        {
            try
            {
                usersSearch.Page.PageIndex = 1;
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SearchDataAsync()
        {
            try
            {
                if (fingerprintManagementFilterModel.UsersId.IsNotNullOrEmpty())
                {
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task YearChangeAsync(string year)
        {
            try
            {
                fingerprintManagementFilterModel.Year = year;
                await SearchDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task SearchMonthChangedAsync(string month)
        {
            try
            {
                fingerprintManagementFilterModel.Month = month;
                await SearchDataAsync();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task PageIndexStaffChangeAsync(PaginationEventArgs e)
        {
            try
            {
                usersSearch.Page.PageIndex = e.Page;
                await LoadUsersAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task PageSizeStaffChangeAsync(PaginationEventArgs e)
        {
            try
            {
                usersSearch.Page.PageIndex = 1;
                usersSearch.Page.PageSize = e.PageSize;
                await LoadUsersAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task OnRowClickAsync(RowData<UsersViewModel> rowData)
        {
            try
            {
                if (fingerprintManagementFilterModel.Year.IsNotNullOrEmpty() && fingerprintManagementFilterModel.Month.IsNotNullOrEmpty() && rowData.IsNotNullOrEmpty())
                {
                    fingerprintManagementFilterModel.UsersId = rowData.Data.Id;
                    await LoadDataAsync();
                }
                else
                {
                    Notice.NotiWarning("Chưa chọn năm, tháng!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void ShowModal()
        {
            try
            {
                excelFilterModel.Year = fingerprintManagementFilterModel.Year;
                excelFilterModel.Month = fingerprintManagementFilterModel.Month;
                fileNameShow = "";
                importVisible = true;
                errorMessage = null;
                FingerprintManagementExcelModels.Clear();
                notExistCodes.Clear();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task HandleOk(MouseEventArgs e)
        {
            try
            {
                confirmLoading = true;
                StateHasChanged();
                errorMessage = null;
                FingerprintManagementExcelModels.Clear();
                notExistCodes.Clear();
                if (fileNameProcess.IsNotNullOrEmpty() && excelFilterModel.Year.IsNotNullOrEmpty() && excelFilterModel.Month.IsNotNullOrEmpty())
                {
                    await using FileStream fs = new FileStream(fileNameProcess, FileMode.Open, FileAccess.Read);
                    await ReadExcelDataAsync(fs);
                }
                else
                {
                    errorMessage = "Chưa chọn năm, tháng!";
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                confirmLoading = false;
                StateHasChanged();
            }
        }

        void HandleCancel(MouseEventArgs e)
        {
            importVisible = false;
            StateHasChanged();
        }

        async Task ReadExcelDataAsync(FileStream fs)
        {
            try
            {
                using ExcelPackage package = new ExcelPackage(fs);
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                List<FingerprintManagement> addFingerprint = new List<FingerprintManagement>();

                int endRows = worksheet.Dimension.End.Row;
                DateTime toDate = new DateTime(excelFilterModel.Year.ToInt(), excelFilterModel.Month.ToInt() + 1, 1).AddDays(-1);
                DateTime fromDate = new DateTime(excelFilterModel.Year.ToInt(), excelFilterModel.Month.ToInt(), 1);

                if (worksheet.Cells[5, 8].Value?.ToString() != "import_fingerprint")
                {
                    errorMessage = "File mẫu không đúng định dạng";
                    return;
                }
                for (int row = 6; row <= endRows; ++row)
                {
                    var lineError = row;
                    string code = worksheet.Cells[row, 1].Value?.ToString();
                    if (code == "0")
                    {
                        continue;
                    }
                    var scanTimeExcel = GetListScanDate(worksheet, row);
                    if (scanTimeExcel?.Any() == true)
                    {
                        foreach (var item in scanTimeExcel)
                        {
                            var fingerprintExist = new FingerprintManagement()
                            {
                                Code = code,
                                ScanDate = item,
                                Year = excelFilterModel.Year.ToInt(),
                                Month = excelFilterModel.Month.ToInt(),
                                Id = ObjectExtentions.GenerateGuid(),
                                CreateDate = DateTime.Now
                            };
                            FingerprintManagementExcelModels.Add(fingerprintExist);
                        }
                    }
                }
                if (FingerprintManagementExcelModels?.Any() != true)
                {
                    errorMessage = "File mẫu không có dữ liệu";
                    return;
                }
                if (FingerprintManagementExcelModels.Any(c => c.Code.IsNullOrEmpty()))
                {
                    errorMessage = "Mã nhân viên không được để trống";
                    return;
                }
                if (FingerprintManagementExcelModels.Any(c => c.ScanDate < fromDate || c.ScanDate > toDate))
                {
                    errorMessage = "Nhập dữ liệu nằm ngoài thời gian đã chọn";
                    return;
                }
                var codes = FingerprintManagementExcelModels.Select(c => c.Code).Distinct().ToList();
                var data = await UsersService.GetAllWithFilterAsync(new UsersSearch() { Codes = codes });
                var usersData = data.Item1;
                if (usersData?.Any() == true && codes.Count() != usersData.Count())
                {
                    var codeInDB = usersData.Select(c => c.Code).ToList();
                    notExistCodes = codes.Except(codeInDB).ToList();
                    errorMessage = "Danh sách mã nhân viên không có trong hệ thống";
                    return;
                }
                var listAdd = FingerprintManagementExcelModels.GroupJoin(usersData, f => f.Code, u => u.Code, (f, u) => (f, u))
                    .Select(x =>
                    {
                        x.f.UsersId = x.u.FirstOrDefault()?.Id;
                        return x.f;
                    }).ToList();

                var result = await FingerprintManagementService.AddListAsync(listAdd);
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    importVisible = false;
                    await SearchAsync();
                }
                else
                {
                    Notice.NotiError("Cập nhật dữ liệu thất bại!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        List<DateTime> GetListScanDate(ExcelWorksheet worksheet, int row)
        {
            try
            {
                List<DateTime> result = new();

                DateTime.TryParse(worksheet.Cells[row, 3].Text, out DateTime scanDate);
                if (scanDate != DateTime.MinValue)
                {
                    for (int i = 4; i <= 7; i++)
                    {
                        DateTime.TryParseExact(worksheet.Cells[row, i].Text, new string[] { "H:mm", "H:mm:ss" }
                        , null, DateTimeStyles.None, out DateTime scanTime);

                        var time = scanTime.TimeOfDay;
                        if (time != TimeSpan.Zero)
                        {
                            var date = scanDate.Date.Add(time);
                            result.Add(date);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                return new();
                throw;
            }
        }

        void ExportFingerprintManagementAsync()
        {
            var fileBase64 = Convert.ToBase64String(GenerateTemplateExcel());
            JSRuntime.SaveAsFile(DateTime.Now.ToString("ddMMyyyy") + "-FileMauLichSuVaoRa.xlsx", fileBase64);
        }

        byte[] GenerateTemplateExcel()
        {
            try
            {
                string fileName = "FileMauLichSuVaoRa.xlsx";
                string pathFile = Path.Combine("wwwroot", "filemau", fileName);

                using (var stream = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
                {
                    var package = new ExcelPackage(stream);
                    return package.GetAsByteArray();
                }
            }
            catch (Exception ex)
            {
                return null;
                throw;
            }
        }

        async Task ImportFingerprintManagementFromExcelAsync(InputFileChangeEventArgs e)
        {

            long maxFileSize = 1024 * 1024 * 50;
            try
            {

                errorMessage = null;
                FingerprintManagementExcelModels.Clear();
                notExistCodes.Clear();
                fileNameShow = e.File.Name;
                foreach (var file in e.GetMultipleFiles(1))
                {
                    if (file.Size > maxFileSize)
                    {
                        Notice.NotiWarning("Tệp quá lớn. Kích thước tối đa cho phép là 10MB!");
                        return;
                    }
                    var trustedFileNameForFileStorage = Path.GetRandomFileName() + ".xlsx";
                    var pathFile = Path.Combine("wwwroot", "input", trustedFileNameForFileStorage);
                    var pathFileSave = Path.Combine("input", trustedFileNameForFileStorage);
                    if (!Directory.Exists(Path.GetDirectoryName(pathFile)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pathFile));
                    }
                    await using FileStream fs = new(pathFile, FileMode.Create, FileAccess.Write);
                    await file.OpenReadStream(maxFileSize).CopyToAsync(fs);
                    fileNameProcess = pathFile;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task ExportExcelAsync()
        {
            try
            {
                excelExporting = true;
                string pathFile = Path.Combine("wwwroot", "filemau", "FileMauLichSuVaoRa.xlsx");
                using (var stream = new FileStream(pathFile, FileMode.Open, FileAccess.Read))
                {
                    var package = new ExcelPackage(stream);
                    var workSheet = package.Workbook.Worksheets[0];

                    var search = Mapper.Map<FingerprintManagementSearch>(fingerprintManagementFilterModel);
                    var data = await FingerprintManagementService.GetAllWithFilterAsync(search);
                    var fingerprintManagements = data.Item1 ?? new List<FingerprintManagement>();
                    var usersDatas = data.Item2 ?? new();
                    var grByStaffDatas = fingerprintManagements.OrderBy(c => c.Users.Name).ThenBy(c => c.ScanDate).ToLookup(c => c.Users.Id).ToList();
                    int i = 6;
                    ExcelRange rangeHeader = workSheet.Cells[6, 1, 6, 7];
                    rangeHeader.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rangeHeader.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.FromArgb(255, 255, 255));
                    workSheet.Cells[6, 8].Value = "";
                    if (grByStaffDatas?.Any() == true)
                    {
                        int endRows = 0;
                        foreach (var grByStaff in grByStaffDatas)
                        {
                            var grDatas = grByStaff.GroupBy(c => c.ScanDate.Value.Date).ToList();
                            endRows += grDatas.Count();
                        }
                        workSheet.InsertRow(7, endRows - 1, 6);
                    }

                    foreach (var grByStaff in grByStaffDatas)
                    {
                        var staff = usersDatas.FirstOrDefault(c => c.Id == grByStaff.Key);
                        var grDatas = grByStaff.GroupBy(c => c.ScanDate.Value.Date).ToList();
                        foreach (var gr in grDatas)
                        {
                            workSheet.Cells[i, 1].Value = staff?.Code;
                            workSheet.Cells[i, 2].Value = staff?.Name;
                            workSheet.Cells[i, 3].Value = gr.Key.ToString("dd/MM/yyyy");
                            int j = 4;
                            foreach (var item in gr.OrderBy(c => c.ScanDate)
                                                    .Select((value, index) => new { index, value }))
                            {
                                int max = gr.Count() - 1;
                                var lstIndex = new List<int>() { max, max - 1, max - 2, max - 3 };
                                if (lstIndex.Contains(item.index))
                                {
                                    workSheet.Cells[i, j].Value = item.value.ScanDate.Value.ToString("HH:mm");
                                    j++;
                                }
                            }
                            i++;
                        }
                    }

                    var fileBase64 = Convert.ToBase64String(package.GetAsByteArray());
                    JSRuntime.SaveAsFile(DateTime.Now.ToString("ddMMyyyy-HHmmss") + "-FileMauBangChamCong.xlsx", fileBase64);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                excelExporting = false;
            }
        }
    }
}
