using System.Collections.Generic;
using PearAdmin.Abp.Auditing.Dto;
using PearAdmin.Abp.CommonDto;

namespace PearAdmin.Abp.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
