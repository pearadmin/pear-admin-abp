using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Auditing;
using PearAdmin.AbpTemplate.Sessions.Dto;

namespace PearAdmin.AbpTemplate.Sessions
{
    public class SessionAppService : AbpTemplateApplicationServiceBase, ISessionAppService
    {
        [DisableAuditing]
        public async Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations()
        {
            var output = new GetCurrentLoginInformationsOutput
            {
                Application = new ApplicationInfoDto
                {
                    Version = AbpTemplateCoreConsts.Version,
                    ReleaseDate = AbpTemplateCoreConsts.ReleaseDate,
                    Features = new Dictionary<string, bool>()
                }
            };

            if (AbpSession.TenantId.HasValue)
            {
                output.Tenant = ObjectMapper.Map<TenantLoginInfoDto>(await GetCurrentTenantAsync());
            }

            if (AbpSession.UserId.HasValue)
            {
                output.User = ObjectMapper.Map<UserLoginInfoDto>(await GetCurrentUserAsync());
            }

            return output;
        }
    }
}
