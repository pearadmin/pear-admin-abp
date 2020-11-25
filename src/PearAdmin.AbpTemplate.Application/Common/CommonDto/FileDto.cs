using System;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 文件Dto
    /// </summary>
    public class FileDto
    {
        public FileDto()
        {

        }

        public FileDto(string fileName, string fileType)
        {
            FileName = fileName;
            FileType = fileType;
            FileToken = Guid.NewGuid().ToString("N");
        }

        [Required]
        public string FileName { get; set; }

        public string FileType { get; set; }

        [Required]
        public string FileToken { get; set; }
    }
}
