using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate.Storage.Minio
{
    [DependsOn(typeof(AbpTemplateStorageModule))]
    public class AbpTemplateMinioStorageModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(AbpTemplateMinioStorageModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}