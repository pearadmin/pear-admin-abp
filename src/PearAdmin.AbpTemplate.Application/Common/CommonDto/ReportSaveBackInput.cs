using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 文档回存请求Dto
    /// </summary>
    public class ReportSaveBackInput
    {
        /// <summary>
        /// 文档Id
        /// </summary>
        public string DocId { get; set; }

        /// <summary>
        /// 本次回存操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 回存默认操作
        /// </summary>
        public string DefaultAction = "saveBack";

        /// <summary>
        /// 回存数据
        /// </summary>
        public SaveBackData Data { get; set; }
    }

    /// <summary>
    /// 回存文档数据
    /// </summary>
    public class SaveBackData
    {
        /// <summary>
        /// 回存文档的ID
        /// </summary>
        public string DocId { get; set; }

        /// <summary>
        /// 修改完最新的文件的下载地址
        /// </summary>
        public string DocURL { get; set; }

        /// <summary>
        /// docURL的base64编码，用于用户对docURL进行校验
        /// </summary>
        public string DocUrlEncode { get; set; }

        /// <summary>
        /// 文件在这次编辑过程中被哪些人修改过
        /// </summary>
        public List<UserInfo> ModifyBy { get; set; }

        /// <summary>
        /// 标示这个文件有没有被修改
        /// </summary>
        public bool Unchanged { get; set; }

        /// <summary>
        /// 文件最新的缩略图
        /// </summary>
        public string PngUrl { get; set; }

        /// <summary>
        /// 文件最新提取的文本
        /// </summary>
        public string TxtUrl { get; set; }

        /// <summary>
        ///  txtUrl的base64编码，用于用户对txUrl进行校验
        /// </summary>
        public string TxtUrlEncode { get; set; }

        /// <summary>
        /// pngUrl的base64编码，用于用户对pngUrl进行校验
        /// </summary>
        public string PngUrlEncode { get; set; }
    }
}
