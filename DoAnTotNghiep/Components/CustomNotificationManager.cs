using AntDesign;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoAnTotNghiep.Components
{
    public class CustomNotificationManager
    {
        readonly NotificationService _notice;
        readonly IConfirmService _confirmService;

        public CustomNotificationManager(NotificationService notice, IConfirmService confirmService)
        {
            _notice = notice;
            _confirmService = confirmService;
        }

        private void NoticeWithIcon(AntDesign.NotificationType type, string message)
        {
            _ = _notice.Open(new NotificationConfig()
            {
                Message = "Thông báo",
                Description = message,
                NotificationType = type,
                Duration = 2,
            });
        }

        public void NotiSuccess(string message)
        {
            NoticeWithIcon(AntDesign.NotificationType.Success, message);
        }

        public void NotiWarning(string message)
        {
            NoticeWithIcon(AntDesign.NotificationType.Warning, message);
        }

        public void NotiError(string message)
        {
            NoticeWithIcon(AntDesign.NotificationType.Error, message);
        }

        public void NotiInfo(string message)
        {
            NoticeWithIcon(AntDesign.NotificationType.Info, message);
        }

        public void NotiNone(string message)
        {
            NoticeWithIcon(AntDesign.NotificationType.None, message);
        }

        public void NotiDeleteError(string message)
        {
            NoticeWithIcon(AntDesign.NotificationType.Error, message);
        }

        public async Task ShowOkWarningAsync(RenderFragment renderFragment)
        {
            await _confirmService.Show(renderFragment,
                "Cảnh cáo",
                ConfirmButtons.OK,
                ConfirmIcon.Warning,
                new ConfirmButtonOptions()
                {
                    Button1Props = new ButtonProps
                    {
                        Type = ButtonType.Primary,
                        ChildContent = "Đồng ý",
                    },
                },
                ConfirmAutoFocusButton.Ok);
        }

        public async Task<ConfirmResult> ShowOkCancelWarningAsync(RenderFragment renderFragment)
        {
            return await _confirmService.Show(renderFragment,
                "Cảnh cáo",
                ConfirmButtons.OKCancel,
                ConfirmIcon.Warning,
                new ConfirmButtonOptions()
                {
                    Button1Props = new ButtonProps
                    {
                        ChildContent = "Đồng ý",
                    },
                    Button2Props = new ButtonProps
                    {
                        ChildContent = "Hủy",
                        Type = ButtonType.Primary
                    },
                },
                ConfirmAutoFocusButton.Cancel);
        }
    }
}
