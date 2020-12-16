using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.Notifications.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Notifications
{
    /// <summary>
    /// 消息应用层服务
    /// </summary>
    public interface INotificationAppService : IApplicationService
    {
        /// <summary>
        /// 分页获取消息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<GetNotificationsOutput> GetPagedUserNotifications(GetUserNotificationsPagedInput input);

        /// <summary>
        /// 未读消息数量
        /// </summary>
        /// <returns></returns>
        Task<int> UnreadMessageCount();

        /// <summary>
        /// 获取消息配置
        /// </summary>
        /// <returns></returns>
        Task<GetNotificationSettingsOutput> GetNotificationSettings();

        /// <summary>
        /// 更新消息配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateNotificationSettings(UpdateNotificationSettingsInput input);

        /// <summary>
        /// 设置指定消息记录已读
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task SetNotificationAsRead(List<EntityDto<Guid>> inputs);

        /// <summary>
        /// 设置所有消息已读
        /// </summary>
        /// <returns></returns>
        Task SetAllNotificationsAsRead();

        /// <summary>
        /// 删除指定消息
        /// </summary>
        /// <param name="inputs"></param>
        /// <returns></returns>
        Task DeleteNotification(List<EntityDto<Guid>> inputs);
    }
}
