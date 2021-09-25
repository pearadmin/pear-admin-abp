using System.Threading.Tasks;
using Abp.Runtime.Caching;
using Abp.UI;
using Castle.Core.Logging;
using PearAdmin.AbpTemplate.ExternalAuth;
using PearAdmin.AbpTemplate.MiniProgram.Enums;
using SKIT.FlurlHttpClient.Wechat.Api;
using SKIT.FlurlHttpClient.Wechat.Api.Models;
using Snowflake.Core;

namespace PearAdmin.AbpTemplate.MiniProgram
{
    public class MiniProgramAuthProviderApi : ExternalAuthProviderApiBase
    {
        public const string ProviderName = "WeChatMiniProgram";
        private WechatApiClient _weChatApiClient;
        private readonly ILogger _logger;
        private readonly ICacheManager _cacheManager;

        public MiniProgramAuthProviderApi(ILogger logger,
            ICacheManager cacheManager)
        {
            _logger = logger;
            _cacheManager = cacheManager;
        }

        public override void Initialize(ExternalLoginProviderInfo providerInfo)
        {
            base.Initialize(providerInfo);
            _weChatApiClient = new WechatApiClient(new WechatApiClientOptions()
            {
                AppId = ProviderInfo.ClientId,
                AppSecret = ProviderInfo.ClientSecret,
            });
        }

        public async override Task<ExternalAuthUserInfo> GetUserInfo(string accessCode)
        {
            var miniProgramSessionKey = await GetWeChatUserOpenId(accessCode);
            var randomId = GenerateRandomId();
            var externalAuthUserInfo = new ExternalAuthUserInfo
            {
                EmailAddress = randomId + $"@{ProviderName}.com",//邮箱唯一性
                Surname = randomId.ToString(),
                Name = randomId.ToString(),
                ProviderKey = miniProgramSessionKey.OpenId,
                Provider = ProviderName,
            };

            return externalAuthUserInfo;
        }

        private long GenerateRandomId()
        {
            var idWorker = new IdWorker(1, 1);
            var id = idWorker.NextId();
            return id;
        }

        private async Task<MiniProgramSessionKey> GetWeChatUserOpenId(string code)
        {
            var response = await _weChatApiClient.ExecuteSnsJsCode2SessionAsync(new SnsJsCode2SessionRequest()
            {
                JsCode = code
            });

            if (response.ErrorCode == (int)RequestErrorCodeEnum.Succeed)
            {
                return new MiniProgramSessionKey()
                {
                    OpenId = response.OpenId,
                    SessionKey = response.SessionKey,
                    UnionId = response.UnionId
                };
            }
            if (response.ErrorCode == (int)RequestErrorCodeEnum.CodeError)
            {
                throw new UserFriendlyException("There was an error calling the mini program api code");
            }

            if (response.ErrorCode == (int)RequestErrorCodeEnum.Busy)
            {
                throw new UserFriendlyException("The wechat mini program api is busy");
            }

            throw new UserFriendlyException("The request for authentication failed");
        }
    }
}
