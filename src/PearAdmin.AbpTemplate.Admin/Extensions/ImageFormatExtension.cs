using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PearAdmin.AbpTemplate.Admin.Helpers
{
    public class ImageFormatExtension
    {
        public static ImageFormat GetRawImageFormat(byte[] fileBytes)
        {
            using (var ms = new MemoryStream(fileBytes))
            {
                var fileImage = Image.FromStream(ms);
                return fileImage.RawFormat;
            }
        }
    }
}
