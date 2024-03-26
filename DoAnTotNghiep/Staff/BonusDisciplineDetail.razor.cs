using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
using DoAnTotNghiep.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Staff
{
    public partial class BonusDisciplineDetail : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] BonusDisciplineService BonusDisciplineService { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        [Inject] PermissionClaim PermissionClaim { get; set; }
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback ValueChanged { get; set; }
        DotNetObjectReference<JSCallback> dotNetRefJSCallback;
        JSCallback jSCallback;
        List<Users> StaffDatas = new List<Users>();
        List<Users> StaffSearchDatas = new List<Users>();
        BonusDisciplineEditModel editModel = new BonusDisciplineEditModel();
        InputWatcher inputWatcher;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadStaffDataAsync();
                jSCallback = new JSCallback { CallbackAction = e => OnDownLoadCompleted(e) };
                dotNetRefJSCallback = DotNetObjectReference.Create(jSCallback);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void LoadEditModel(BonusDiscipline value, bool readOnly = false)
        {
            try
            {
                editModel = Mapper.Map<BonusDisciplineEditModel>(value);
                editModel.ReadOnly = readOnly;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task GetFileAttachAsync(BonusDisciplineEditModel model)
        {
            try
            {
                if (model.PathAttachFile.IsNullOrEmpty())
                {
                    return;
                }
                await JSRuntime.DownloadFileFromUrl(
                    model.PathAttachFile,
                    "Get",
                    null,
                    model.AttachFileName,
                    dotNetRefJSCallback,
                    jSCallback.GetNameCallbackMethod());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnDownLoadCompleted(object data)
        {
            try
            {
                if (data == null)
                {
                    Notice.NotiError("Tải file thất bại!");
                }
                else
                {
                    Notice.NotiSuccess("Tải file thành công!");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            StateHasChanged();
        }

        void RemoveFile()
        {
            try
            {
                editModel.PathAttachFile = null;
                editModel.AttachFileName = null;
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task ImportFileAsync(InputFileChangeEventArgs e)
        {
            try
            {
                int maxAllowSize = 1024 * 1024 * 10;
                foreach (var file in e.GetMultipleFiles(1))
                {
                    if (file.Size > maxAllowSize)
                    {
                        Notice.NotiWarning("Tệp quá lớn. Kích thước tối đa cho phép là 10MB!");
                        return;
                    }
                    var extension = Path.GetExtension(file.Name);
                    var trustedFileNameForFileStorage = ObjectExtentions.GenerateGuid() + extension;
                    var pathFile = Path.Combine("wwwroot", "attachFile", trustedFileNameForFileStorage);
                    var pathFileSave = Path.Combine("attachFile", trustedFileNameForFileStorage);
                    if (!Directory.Exists(Path.GetDirectoryName(pathFile)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pathFile));
                    }
                    await using FileStream fs = new(pathFile, FileMode.Create, FileAccess.Write);
                    await file.OpenReadStream(maxAllowSize).CopyToAsync(fs);
                    editModel.PathAttachFile = pathFileSave;
                    editModel.AttachFileName = file.Name;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void Edit()
        {
            editModel.ReadOnly = false;
        }

        async Task SaveAsync()
        {
            try
            {
                bool result = new();
                var errorMessageStore = editModel.ValidateAll();
                if (!inputWatcher.Validate() || errorMessageStore.Any())
                {
                    if (errorMessageStore.Any())
                    {
                        inputWatcher.NotifyFieldChanged(errorMessageStore.First().Key, errorMessageStore);
                    }
                    Notice.NotiWarning("Dữ liệu còn thiếu hoặc không hợp lệ!");
                    return;
                }
                var existData = await BonusDisciplineService.GetAllWithFilterAsync(new BonusDisciplineSearch()
                {
                    Code = editModel.Code,
                    CheckExist = true
                });
                if (existData.Item1?.Any(c => c.Id != editModel.Id) == true)
                {
                    Notice.NotiWarning("Số quyết định đã được sử dụng");
                    return;
                }
                var bonusDiscipline = Mapper.Map<BonusDiscipline>(editModel);
                if (bonusDiscipline.Id.IsNotNullOrEmpty())
                {
                    result = await BonusDisciplineService.UpdateAsync(bonusDiscipline);
                }
                else
                {
                    bonusDiscipline.Id = ObjectExtentions.GenerateGuid();
                    bonusDiscipline.CreateDate = DateTime.Now;
                    result = await BonusDisciplineService.AddAsync(bonusDiscipline);
                }
                if (result)
                {
                    Notice.NotiSuccess("Cập nhật dữ liệu thành công!");
                    await ValueChanged.InvokeAsync();
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

        async Task CancelClickAsync()
        {
            editModel = new BonusDisciplineEditModel();
            await Cancel.InvokeAsync();
        }

        async Task LoadStaffDataAsync()
        {
            try
            {
                var dataStaff = await UsersService.GetAllWithFilterAsync(new UsersSearch());
                StaffDatas = dataStaff.Item1 ?? new();
                StaffSearchDatas = StaffDatas.Take(5).ToList();
                if (editModel.UsersId.IsNotNullOrEmpty())
                {
                    var crStaff = StaffDatas.FirstOrDefault(c => c.Id == editModel.UsersId);
                    if (crStaff != null)
                    {
                        StaffSearchDatas.Add(crStaff);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void StaffChange(string staffId)
        {
            try
            {
                var selectedStaff = StaffDatas.FirstOrDefault(c => c.Id == staffId);
                if (selectedStaff != null)
                {
                    editModel.UsersData = Mapper.Map<UsersEditModel>(selectedStaff);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void OnStaffSearch(string e)
        {
            try
            {
                StaffSearchDatas.Clear();
                StaffSearchDatas = StaffDatas.Where(c => c.Name.Contains(e, StringComparison.OrdinalIgnoreCase)).ToList();
                StateHasChanged();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
