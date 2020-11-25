using System;
using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.Social.Chat.Dto
{
    public class ChatUserDto : EntityDto<long>
    {
        public Guid? ProfilePictureId { get; set; }

        public string UserName { get; set; }

        public int UnreadMessageCount { get; set; }

        public bool IsOnline { get; set; }
    }
}