using System.Collections.Generic;

namespace PearAdmin.Abp.Social.Chat.Dto
{
    public class ChatUserWithMessagesDto : ChatUserDto
    {
        public List<ChatMessageDto> Messages { get; set; }
    }
}