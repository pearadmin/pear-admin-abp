using Abp.Modules;
using PearAdmin.AbpTemplate.ExternalAuth;

namespace PearAdmin.AbpTemplate.MiniProgram
{
    [DependsOn(typeof(AbpTemplateExternalAuthModule))]
    public class AbpTemplateMiniProgramModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateMiniProgramModule).Assembly);
        }
    }
}
