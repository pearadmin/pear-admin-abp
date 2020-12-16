using System.IO;
using System.Threading.Tasks;
using Abp.Dependency;

namespace PearAdmin.AbpTemplate.Storage
{
    /// <summary>
    /// 存储服务
    /// </summary>
    public interface IStorageManager : ITransientDependency
    {
        /// <summary>
        /// 提供程序名称
        /// </summary>
        string ProviderName { get; }

        Task UploadObject(string objectName, string filePath, string contentType, string bucketName = "default", string location = "us-east-1");

        Task UploadObject(string objectName, Stream data, string contentType, string bucketName = "default", string location = "us-east-1");

        Task<string> UploadObjectUrlAsync(string objectName, int expiresInt = 604800, string bucketName = "default");

        Task<string> GetObjectUrlAsync(string objectName, int expiresInt = 604800, string bucketName = "default");
    }
}
