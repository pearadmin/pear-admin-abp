using Abp;
using Abp.Modules;

namespace PearAdmin.AbpTemplate.ExternalAuth
{
    [DependsOn(typeof(AbpKernelModule))]
    public class AbpTemplateExternalAuthModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateExternalAuthModule).Assembly);
        }
    }
}
