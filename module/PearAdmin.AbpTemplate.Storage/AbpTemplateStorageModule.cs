using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate.Storage
{
    public class AbpTemplateStorageModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(AbpTemplateStorageModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}
