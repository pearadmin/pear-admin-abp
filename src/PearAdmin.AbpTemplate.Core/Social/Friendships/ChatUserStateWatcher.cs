using System.Linq;
using Abp;
using Abp.Dependency;
using Abp.RealTime;
using Abp.Threading;
using PearAdmin.AbpTemplate.Social.Chat;
using PearAdmin.AbpTemplate.Social.Friendships.Cache;

namespace PearAdmin.AbpTemplate.Social.Friendships
{
    public class ChatUserStateWatcher : ISingletonDependency
    {
        private readonly IChatCommunicator _chatCommunicator;
        private readonly IUserFriendsCache _userFriendsCache;
        private readonly IOnlineClientManager<ChatChannel> _onlineClientManager;

        public ChatUserStateWatcher(
            IChatCommunicator chatCommunicator,
            IUserFriendsCache userFriendsCache,
            IOnlineClientManager<ChatChannel> onlineClientManager)
        {
            _chatCommunicator = chatCommunicator;
            _userFriendsCache = userFriendsCache;
            _onlineClientManager = onlineClientManager;
        }

        public void Initialize()
        {
            _onlineClientManager.UserConnected += OnlineClientManager_UserConnected;
            _onlineClientManager.UserDisconnected += OnlineClientManager_UserDisconnected;
        }

        private void OnlineClientManager_UserConnected(object sender, OnlineUserEventArgs e)
        {
            NotifyUserConnectionStateChange(e.User, true);
        }

        private void OnlineClientManager_UserDisconnected(object sender, OnlineUserEventArgs e)
        {
            NotifyUserConnectionStateChange(e.User, false);
        }

        private void NotifyUserConnectionStateChange(UserIdentifier user, bool isConnected)
        {
            var onlineUserClients = _onlineClientManager.GetAllClients();
            if (onlineUserClients.Any())
            {
                AsyncHelper.RunSync(() => _chatCommunicator.SendUserConnectionChangeToClients(onlineUserClients, user, isConnected));
            }
        }
    }
}
