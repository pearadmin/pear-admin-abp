using Abp.Modules;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate.Storage.Aliyun
{
    public class AliyunStorageModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(AliyunStorageModule).GetAssembly());
        }
    }
}
