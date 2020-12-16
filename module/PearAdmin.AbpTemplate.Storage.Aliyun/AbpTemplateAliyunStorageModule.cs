using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate.Storage.Aliyun
{
    [DependsOn(typeof(AbpTemplateStorageModule))]
    public class AbpTemplateAliyunStorageModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AbpTemplateAliyunStorageModule).GetAssembly());
        }
    }
}
