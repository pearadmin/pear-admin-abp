using System;
using System.Collections.Generic;
using Castle.Components.DictionaryAdapter;
using PearAdmin.AbpTemplate.Social.Friendships.Dto;

namespace PearAdmin.AbpTemplate.Social.Chat.Dto
{
    public class GetUserChatFriendsWithSettingsOutput
    {
        public DateTime ServerTime { get; set; }
        
        public List<FriendDto> Friends { get; set; }

        public GetUserChatFriendsWithSettingsOutput()
        {
            Friends = new EditableList<FriendDto>();
        }
    }
}