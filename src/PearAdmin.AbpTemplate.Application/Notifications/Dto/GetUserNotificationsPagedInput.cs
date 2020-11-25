using Abp.Notifications;
using PearAdmin.AbpTemplate.CommonDto;

namespace PearAdmin.AbpTemplate.Notifications.Dto
{
    public class GetUserNotificationsPagedInput : PagedInputDto
    {
        public UserNotificationState? State { get; set; }
    }
}
