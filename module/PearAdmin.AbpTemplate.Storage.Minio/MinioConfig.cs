using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.Storage.Minio
{
    /// <summary>
    /// Minio配置类
    /// </summary>
    public class MinioConfig
    {
        /// <summary>
        /// Bucket name
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// Location of the server, supports HTTP and HTTPS
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// Access Key for authenticated requests (Optional, can be omitted for anonymous requests)
        /// </summary>
        [Required]
        public string AccessKey { get; set; }

        /// <summary>
        /// Secret Key for authenticated requests (Optional, can be omitted for anonymous requests)
        /// </summary>
        [Required]
        public string SecretKey { get; set; }

        /// <summary>
        /// Optional custom region
        /// </summary>
        public string Region { get; set; } = "";

        /// <summary>
        /// Optional session token
        /// </summary>
        public string SessionToken { get; set; } = "";
    }
}
