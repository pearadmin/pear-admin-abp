using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.Abp.Storage
{
    /// <summary>
    /// 临时文件缓存领域服务
    /// </summary>
    public interface ITempFileCacheManager:ITransientDependency
    {
        void SetFile(string token, byte[] content);

        byte[] GetFile(string token);
    }
}
