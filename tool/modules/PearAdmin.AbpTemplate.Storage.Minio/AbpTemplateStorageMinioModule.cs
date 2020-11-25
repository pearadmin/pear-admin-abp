using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate.Storage.Minio
{
    [DependsOn(typeof(AbpTemplateStorageModule))]
    public class AbpTemplateStorageMinioModule : AbpModule
    {
        public override void Initialize()
        {
            var thisAssembly = typeof(AbpTemplateStorageMinioModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);
        }
    }
}