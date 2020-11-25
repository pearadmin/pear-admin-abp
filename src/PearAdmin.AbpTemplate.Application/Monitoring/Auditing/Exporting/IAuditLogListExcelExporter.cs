using System.Collections.Generic;
using PearAdmin.AbpTemplate.Auditing.Dto;
using PearAdmin.AbpTemplate.CommonDto;

namespace PearAdmin.AbpTemplate.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
