using Abp.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace PearAdmin.Abp.CommonDto
{
    /// <summary>
    /// 报告预览参数Dto
    /// </summary>
    public class ReportPrepareParamDto
    {
        /// <summary>
        /// 文档信息
        /// </summary>
        public DocInfo Doc { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public UserInfo User { get; set; }
    }

    /// <summary>
    /// 编辑预览的文档的信息
    /// </summary>
    public class DocInfo
    {
        /// <summary>
        /// 文档的ID，不同的文档通过docId 来区分。
        /// </summary>
        public string DocId { get; set; }

        /// <summary>
        /// 文件的标题，用于编辑时在编辑器上显示当前文件的标题，需包含加上文件扩展名
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 文件的类型
        /// </summary>
        public string Mime_type { get; set; }

        /// <summary>
        /// 文档下载地址(该链接不要进行编码)
        /// </summary>
        public string FetchUrl { get; set; }

        /// <summary>
        /// 文件的缩略图
        /// </summary>
        public string Thumbnail { get; set; }

        /// <summary>
        /// 该值true时，Office文件将使用pdf模式进行预览。默认为false，将使用Office预览
        /// </summary>
        public bool Pdf_viewer { get; set; }

        /// <summary>
        /// API模式下必须为true
        /// </summary>
        public bool FromApi { get; set; }

        /// <summary>
        /// API模式下可以通过改参数指定文档的回调地址，也可以不传递该参数而通过配置文件统一配置回存地址。
        /// </summary>
        public string Callback { get; set; }
    }

    /// <summary>
    /// 文档类型辅助类
    /// </summary>
    public class MineTypeHelper
    {
        /// <summary>
        /// 依照文件后缀名获取MimeType
        /// </summary>
        /// <param name="">文件后缀名</param>
        /// <returns></returns>
        public static string GetMineTypeValue(string fileNameSuffix)
        {
            /// <summary>
            /// 毕升文档常用MimeType
            /// </summary>
            Dictionary<string, string> MimeTypes = new Dictionary<string, string>()
            {
                //office
                { "doc", "application/msword"},
                { "docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                { "xls", "application/vnd.ms-excel"},
                { "xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                { "ppt", "application/vnd.ms-powerpoint"},
                { "pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},

                //pdf
                { "pdf", "application/pdf"},

                //文本文件
                { "html", "text/plain"},
                { "xml", "text/plain"},
                { "js", "text/plain"},
                { "css", "text/plain"},
                { "log", "text/plain"},

                //音频文件
                { "mp3","audio/mpeg"},
                //{ "ogg","audio/ogg" },
                { "wav","audio/wav" },

                //视频文件
                { "mp4","video/mp4"},
                { "webm","video/webm" },
                { "ogg","video/ogg" },

                //图片文件
                { "png","image/png"},
                { "jpeg","image/jpeg" },
                { "jpg","image/jpeg" }
            };

            var mimeTypeKey = MimeTypes.Keys.Where(m => m == fileNameSuffix).FirstOrDefault();
            if (mimeTypeKey.IsNullOrEmpty())
            {
                return mimeTypeKey;
            }

            var mimeTypeValue = MimeTypes[mimeTypeKey];
            return mimeTypeValue;
        }
    }

    /// <summary>
    /// 当前编辑预览文档的用户的信息
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 用户ID，不同的用户通过uid来做区分
        /// </summary>
        public string Uid { get; set; }

        /// <summary>
        /// api调用模式下无特别意义，保持和uid一致即可
        /// </summary>
        public string Oid { get; set; }

        /// <summary>
        /// 用户昵称。用于多人编辑时标示不同的用户。
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 用户头像地址。用于多人编辑时 在右上角显示各个用户。
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户对目前编辑/预览的文件有何种权限
        /// </summary>
        public List<string> Privilege { get; set; }

        /// <summary>
        /// 可为空，在自定义UI是将会使用到该值
        /// </summary>
        public Option Opts { get; set; }
    }

    /// <summary>
    /// Ui自定义选项
    /// </summary>
    public class Option
    {

    }

    /// <summary>
    /// 用户针对文档权限设定
    /// 如果你需要在在预览模式隐藏编辑以及下载按钮，可以不传递FILE_WRITE以及FILE_DOWNLOAD
    /// </summary>
    public class PrivilegeType
    {
        /// <summary>
        /// 读取权限
        /// </summary>
        public static string readFile = "FILE_READ";

        /// <summary>
        /// 写入权限
        /// </summary>
        public static string writeFile = "FILE_WRITE";

        /// <summary>
        /// 下载权限
        /// </summary>
        public static string downLoadFile = "FILE_DOWNLOAD";

        /// <summary>
        /// 打印权限
        /// </summary>
        public static string printFile = "FILE_PRINT";
    }
}
