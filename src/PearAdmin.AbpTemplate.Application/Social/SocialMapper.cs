using AutoMapper;
using PearAdmin.AbpTemplate.Social.Chat;
using PearAdmin.AbpTemplate.Social.Chat.Dto;
using PearAdmin.AbpTemplate.Social.Friendships;
using PearAdmin.AbpTemplate.Social.Friendships.Cache;
using PearAdmin.AbpTemplate.Social.Friendships.Dto;

namespace PearAdmin.AbpTemplate.Social
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
