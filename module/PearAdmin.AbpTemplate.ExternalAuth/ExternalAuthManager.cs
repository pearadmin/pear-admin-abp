using System;
using System.Linq;
using System.Threading.Tasks;
using Abp.Dependency;

namespace PearAdmin.AbpTemplate.ExternalAuth
{
    public class ExternalAuthManager : IExternalAuthManager, ITransientDependency
    {
        private readonly IIocResolver _iocResolver;
        private readonly IExternalAuthConfiguration _externalAuthConfiguration;

        public ExternalAuthManager(IIocResolver iocResolver, IExternalAuthConfiguration externalAuthConfiguration)
        {
            _iocResolver = iocResolver;
            _externalAuthConfiguration = externalAuthConfiguration;
        }

        public Task<bool> IsValidUser(string provider, string providerKey, string providerAccessCode)
        {
            using (IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> providerApi = CreateProviderApi(provider))
                return providerApi.Object.IsValidUser(providerKey, providerAccessCode);
        }

        public Task<ExternalAuthUserInfo> GetUserInfo(string provider, string accessCode)
        {
            using (IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> providerApi = CreateProviderApi(provider))
                return providerApi.Object.GetUserInfo(accessCode);
        }

        public IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> CreateProviderApi(string provider)
        {
            var providerInfo = _externalAuthConfiguration.Providers.Single(infoProvider => infoProvider.Name == provider).GetExternalLoginInfo();
            if (providerInfo == null)
                throw new Exception("Unknown external auth provider: " + provider);
            IDisposableDependencyObjectWrapper<IExternalAuthProviderApi> dependencyObjectWrapper = IocResolverExtensions.ResolveAsDisposable<IExternalAuthProviderApi>(_iocResolver, providerInfo.ProviderApiType);
            dependencyObjectWrapper.Object.Initialize(providerInfo);
            return dependencyObjectWrapper;
        }
    }
}
