using Abp.Notifications;
using PearAdmin.Abp.CommonDto;

namespace PearAdmin.Abp.Notifications.Dto
{
    public class GetUserNotificationsPagedInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}
