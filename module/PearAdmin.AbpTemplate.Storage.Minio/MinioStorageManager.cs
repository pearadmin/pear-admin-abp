using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Minio;

namespace PearAdmin.AbpTemplate.Storage.Minio
{
    /// <summary>
    /// Minio存储服务
    /// </summary>
    public class MinioStorageManager : IStorageManager
    {
        private readonly MinioConfig _minioConfig;
        private readonly MinioClient _minioClient;
        private readonly IConfiguration _configuration;

        public string ProviderName => "MinioStorage";

        public MinioStorageManager(IConfiguration configuration)
        {
            _configuration = configuration;

            _minioConfig = new MinioConfig()
            {
                Endpoint = _configuration.GetSection($"{ProviderName}:Endpoint").Value,
                AccessKey = _configuration.GetSection($"{ProviderName}:AccessKey").Value,
                SecretKey = _configuration.GetSection($"{ProviderName}:SecretKey").Value,
            };
            _minioClient = MinioClientFactory.Create(_minioConfig);
        }

        public async Task UploadObject(string objectName, string filePath, string contentType, string bucketName = "default", string location = "us-east-1")
        {
            // 检查是否存在bucket
            bool found = await _minioClient.BucketExistsAsync(bucketName);
            if (!found)
            {
                // 创建bucket
                await _minioClient.MakeBucketAsync(bucketName, location);
            }

            // 上传文件到bucket
            await _minioClient.PutObjectAsync(bucketName, objectName, filePath, contentType);
        }

        public async Task UploadObject(string objectName, Stream data, string contentType, string bucketName = "default", string location = "us-east-1")
        {
            // 检查是否存在bucket
            bool found = await _minioClient.BucketExistsAsync(bucketName);
            if (!found)
            {
                // 创建bucket
                await _minioClient.MakeBucketAsync(bucketName, location);
            }

            // 上传文件到bucket
            await _minioClient.PutObjectAsync(bucketName, objectName, data, data.Length, contentType);
        }

        public async Task<string> UploadObjectUrlAsync(string objectName, int expiresInt = 604800, string bucketName = "default")
        {
            var fileUrl = await _minioClient.PresignedPutObjectAsync(bucketName, objectName, expiresInt);
            return fileUrl;
        }

        public async Task<string> GetObjectUrlAsync(string objectName, int expiresInt = 604800, string bucketName = "default")
        {
            var fileUrl = await _minioClient.PresignedGetObjectAsync(bucketName, objectName, expiresInt);
            return fileUrl;
        }
    }
}
