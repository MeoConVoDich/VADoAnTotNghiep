using AntDesign;
using AutoMapper;
using DoAnTotNghiep.Components;
using DoAnTotNghiep.Config;
using DoAnTotNghiep.Domain;
using DoAnTotNghiep.EditModel;
using DoAnTotNghiep.SearchModel;
using DoAnTotNghiep.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Staff
{
    public partial class StaffProfileDetail : ComponentBase
    {
        [Inject] IMapper Mapper { get; set; }
        [Inject] CustomNotificationManager Notice { get; set; }
        [Inject] UsersService UsersService { get; set; }
        [CascadingParameter] StaffProfileViewMode StaffProfileViewMode { get; set; }
        [Parameter] public EventCallback Cancel { get; set; }
        [Parameter] public EventCallback ValueChanged { get; set; }
        UsersEditModel editModel = new UsersEditModel();
        InputWatcher inputWatcher;
        bool hasUserName = false;
        public void LoadEditModel(Users value, bool readOnly = false)
        {
            try
            {
                if (value.UserName.IsNotNullOrEmpty())
                {
                    hasUserName = true;
                }
                else
                {
                    hasUserName = false;
                }
                editModel = Mapper.Map<UsersEditModel>(value);
                editModel.ReadOnly = readOnly;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        async Task ImportAvatarAsync(InputFileChangeEventArgs e)
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
                    var pathFile = Path.Combine("wwwroot", "image" ,trustedFileNameForFileStorage);
                    var pathFileSave = Path.Combine("image" ,trustedFileNameForFileStorage);
                    if (!Directory.Exists(Path.GetDirectoryName(pathFile)))
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(pathFile));
                    }
                    await using FileStream fs = new(pathFile, FileMode.Create, FileAccess.Write);
                    await file.OpenReadStream(maxAllowSize).CopyToAsync(fs);
                    editModel.Avatar = pathFileSave;
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
            var existData = await UsersService.GetAllWithFilterAsync(new UsersSearch() 
            {
                Code = editModel.Code, 
                IdentityNumber = editModel.IdentityNumber,
                CheckExist = true,
                UserName = editModel.UserName
            });
            if (existData.Item1?.Any(c => c.Id != editModel.Id) == true)
            {
                Notice.NotiWarning("Mã nhân viên hoặc CMND/CCCD hoặc tên đăng nhập trùng với nhân viên khác!");
                return;
            }
            if (editModel.UserName.IsNotNullOrEmpty() && !hasUserName)
            {
                editModel.Password = "123456aA@";
                editModel.IsAdmin = true;
            }
            var users = Mapper.Map<Users>(editModel);
            if (users.Id.IsNotNullOrEmpty())
            {
                result = await UsersService.UpdateAsync(users);
            }
            else
            {
                users.Id = ObjectExtentions.GenerateGuid();
                result = await UsersService.AddAsync(users);
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

        async Task CancelClickAsync()
        {
            editModel = new UsersEditModel();
            await Cancel.InvokeAsync();
        }

    }
}
