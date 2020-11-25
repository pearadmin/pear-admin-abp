using Abp.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PearAdmin.Abp.IO
{
    /// <summary>
    /// 应用程序文件辅助类
    /// </summary>
    public static class AppFileHelper
    {
        public static string ParamsPath(params object[] strs)
        {
            string path = string.Empty;

            strs.ToList().ForEach((str) =>
            {
                if (str != null)
                {
                    path += "\\" + str.ToString();
                }
            });
            path = path.TrimStart('\\');
            return AppContext.BaseDirectory + path;
        }

        public static string CheckFileIsExsit(string Path, string fileName, string ContentType, int RepeatCount = 0)
        {
            //判断文件是否存在
            if (File.Exists(Path + "/" + fileName))
            {
                if (RepeatCount > 0)
                {
                    RepeatCount++;//重复次数+1
                    fileName = fileName.Replace("(" + (RepeatCount - 1) + ")", "(" + RepeatCount + ")");
                }
                else
                {
                    RepeatCount++;//重复次数+1
                    fileName = fileName.Substring(0, fileName.LastIndexOf('.')) + "(" + RepeatCount + ")" + fileName.Substring(fileName.LastIndexOf("."));
                }
                return CheckFileIsExsit(Path, fileName, ContentType, RepeatCount);

            }
            return fileName;
        }

        public static IEnumerable<string> ReadLines(string path)
        {
            using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
            using (var sr = new StreamReader(fs, Encoding.UTF8))
            {
                string line;
                while ((line = sr.ReadLine()) != null) yield return line;
            }
        }

        public static void DeleteFilesInFolderIfExists(string folderPath, string fileNameWithoutExtension)
        {
            var directory = new DirectoryInfo(folderPath);
            var tempUserProfileImages = directory.GetFiles($"{fileNameWithoutExtension}.*", SearchOption.AllDirectories)
                .ToList();
            foreach (var tempUserProfileImage in tempUserProfileImages)
                FileHelper.DeleteIfExists(tempUserProfileImage.FullName);
        }
    }
}