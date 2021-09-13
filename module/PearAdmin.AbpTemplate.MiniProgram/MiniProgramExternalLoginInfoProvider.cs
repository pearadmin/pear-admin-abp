using PearAdmin.AbpTemplate.ExternalAuth;

namespace PearAdmin.AbpTemplate.MiniProgram
{
    public class MiniProgramExternalLoginInfoProvider : IExternalLoginInfoProvider
    {
        public string Name { get; } = "WeChatMiniProgram";

        protected string ConsumerKey { get; set; }

        protected string ConsumerSecret { get; set; }

        protected ExternalLoginProviderInfo ExternalLoginProviderInfo { get; set; }

        public MiniProgramExternalLoginInfoProvider(string appId, string appSecret)
        {
            ConsumerKey = appId;
            ConsumerSecret = appSecret;
            ExternalLoginProviderInfo = new ExternalLoginProviderInfo(Name, ConsumerKey, ConsumerSecret, typeof(MiniProgramAuthProviderApi), null);
        }

        public virtual ExternalLoginProviderInfo GetExternalLoginInfo()
        {
            return ExternalLoginProviderInfo;
        }
    }
}
