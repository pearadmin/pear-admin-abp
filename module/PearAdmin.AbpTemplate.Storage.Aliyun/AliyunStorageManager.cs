using System;
using System.IO;
using System.Threading.Tasks;
using Aliyun.OSS;
using Aliyun.OSS.Util;
using Microsoft.Extensions.Configuration;

namespace PearAdmin.AbpTemplate.Storage.Aliyun
{
    public class AliyunStorageManager : IStorageManager
    {
        private readonly AliyunConfig _aliyunConfig;
        private readonly OssClient _ossClient;
        private readonly IConfiguration _configuration;

        public string ProviderName => "AliyunStorage";

        public AliyunStorageManager(IConfiguration configuration)
        {
            _configuration = configuration;
            _aliyunConfig = new AliyunConfig()
            {
                Endpoint = _configuration.GetSection($"{ProviderName}:Endpoint").Value,
                AccessKeyId = _configuration.GetSection($"{ProviderName}:AccessKeyId").Value,
                AccessKeySecret = _configuration.GetSection($"{ProviderName}:AccessKeySecret").Value,
            };
            _ossClient = AliyunClientFactory.Create(_aliyunConfig);
        }

        public Task UploadObject(string objectName, string filePath, string contentType, string bucketName = "default", string location = "us-east-1")
        {
            throw new NotImplementedException();
        }

        public async Task UploadObject(string objectName, Stream data, string contentType, string bucketName = "default", string location = "us-east-1")
        {
            await Task.Run(() =>
            {
                var key = $"{bucketName}/{objectName}";
                var md5 = OssUtils.ComputeContentMd5(data, data.Length);
                var objectMeta = new ObjectMetadata();
                objectMeta.AddHeader("Content-MD5", md5);
                objectMeta.UserMetadata.Add("Content-MD5", md5);
                _ossClient.PutObject(bucketName, key, data, objectMeta);
            });
        }

        public Task<string> UploadObjectUrlAsync(string objectName, int expiresInt = 604800, string bucketName = "default")
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetObjectUrlAsync(string objectName, int expiresInt = 604800, string bucketName = "default")
        {
            return await Task.Run(() => $"{_aliyunConfig.Endpoint}/{bucketName}/{objectName}");
        }
    }
}
