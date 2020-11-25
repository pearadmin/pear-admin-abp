using Minio;

namespace PearAdmin.AbpTemplate.Storage.Minio
{
    public class MinioClientFactory
    {
        public static MinioClient Create(MinioConfig minioConfig)
        {
            var minioClient = new MinioClient(minioConfig.Endpoint, minioConfig.AccessKey, minioConfig.SecretKey, minioConfig.Region, minioConfig.SessionToken);

            return minioClient;
        }
    }
}
