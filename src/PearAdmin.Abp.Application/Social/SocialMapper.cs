using AutoMapper;
using PearAdmin.Abp.Social.Chat;
using PearAdmin.Abp.Social.Chat.Dto;
using PearAdmin.Abp.Social.Friendships;
using PearAdmin.Abp.Social.Friendships.Cache;
using PearAdmin.Abp.Social.Friendships.Dto;

namespace PearAdmin.Abp.Social
{
    internal static class SocialMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Friendship, FriendDto>();
            configuration.CreateMap<FriendCacheItem, FriendDto>();
            configuration.CreateMap<ChatMessage, ChatMessageDto>();
        }
    }
}
