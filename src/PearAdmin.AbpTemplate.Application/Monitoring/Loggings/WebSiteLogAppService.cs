using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using PearAdmin.AbpTemplate.CommonDto;
using PearAdmin.AbpTemplate.IO;
using PearAdmin.AbpTemplate.Loggings.Dto;
using PearAdmin.AbpTemplate.Net.MimeTypes;
using PearAdmin.AbpTemplate.TempFileCaches;

namespace PearAdmin.AbpTemplate.Loggings
{
    public class WebSiteLogAppService : AbpTemplateApplicationServiceBase, IWebSiteLogAppService
    {
        private readonly IAppFolders _appFolders;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        public WebSiteLogAppService(IAppFolders appFolders, ITempFileCacheManager tempFileCacheManager)
        {
            _appFolders = appFolders;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public GetLatestWebLogsOutput GetLatestWebLogs()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);

            if (!directory.Exists)
            {
                return new GetLatestWebLogsOutput
                {
                    LatestWebLogLines = new List<string>()
                };
            }

            var lastLogFile = directory.GetFiles("*.txt", SearchOption.AllDirectories)
                .OrderByDescending(f => f.LastWriteTime)
                .FirstOrDefault();

            if (lastLogFile == null)
            {
                return new GetLatestWebLogsOutput();
            }

            var lines = AppFileHelper.ReadLines(lastLogFile.FullName).Reverse().Take(1000).ToList();
            var logLineCount = 0;
            var lineCount = 0;

            foreach (var line in lines)
            {
                if (line.StartsWith("DEBUG") ||
                    line.StartsWith("INFO") ||
                    line.StartsWith("WARN") ||
                    line.StartsWith("ERROR") ||
                    line.StartsWith("FATAL"))
                    logLineCount++;

                lineCount++;

                if (logLineCount == 100) break;
            }

            return new GetLatestWebLogsOutput
            {
                LatestWebLogLines = lines.Take(lineCount).Reverse().ToList()
            };
        }

        public FileDto DownloadWebLogs()
        {
            var logFiles = GetAllLogFiles();

            var zipFileDto = new FileDto("WebSiteLogs.zip", MimeTypeNames.ApplicationZip);

            using (var outputZipFileStream = new MemoryStream())
            {
                using (var zipStream = new ZipArchive(outputZipFileStream, ZipArchiveMode.Create))
                {
                    foreach (var logFile in logFiles)
                    {
                        var entry = zipStream.CreateEntry(logFile.Name);
                        using (var entryStream = entry.Open())
                        {
                            using (var fs = new FileStream(logFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite, 0x1000, FileOptions.SequentialScan))
                            {
                                fs.CopyTo(entryStream);
                                entryStream.Flush();
                            }
                        }
                    }
                }

                _tempFileCacheManager.SetFile(zipFileDto.FileToken, outputZipFileStream.ToArray());
            }

            return zipFileDto;
        }

        private List<FileInfo> GetAllLogFiles()
        {
            var directory = new DirectoryInfo(_appFolders.WebLogsFolder);
            return directory.GetFiles("*.*", SearchOption.TopDirectoryOnly).ToList();
        }
    }
}