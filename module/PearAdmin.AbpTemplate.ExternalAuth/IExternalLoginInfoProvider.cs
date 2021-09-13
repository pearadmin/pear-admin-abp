namespace PearAdmin.AbpTemplate.ExternalAuth
{
    public interface IExternalLoginInfoProvider
    {
        string Name { get; }

        ExternalLoginProviderInfo GetExternalLoginInfo();
    }
}
