using Aliyun.OSS;

namespace PearAdmin.AbpTemplate.Storage.Aliyun
{
    public class AliyunClientFactory
    {
        public static OssClient Create(AliyunConfig aliyunConfig)
        {
            var ossClient = new OssClient(aliyunConfig.Endpoint, aliyunConfig.AccessKeyId, aliyunConfig.AccessKeySecret);

            return ossClient;
        }
    }
}
