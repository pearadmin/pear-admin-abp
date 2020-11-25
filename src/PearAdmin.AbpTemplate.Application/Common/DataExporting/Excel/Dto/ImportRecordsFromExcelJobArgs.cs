using Abp;
using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.AbpTemplate.DataExporting.Excel.Dto
{
    /// <summary>
    /// 从Excel导入记录请求参数
    /// </summary>
    public class ImportRecordsFromExcelJobArgs
    {
        public int? TenantId { get; set; }

        public Guid BinaryObjectId { get; set; }

        public UserIdentifier User { get; set; }

        public byte[] Files { get; set; }
    }
}
