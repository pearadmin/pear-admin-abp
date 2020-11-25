using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PearAdmin.Abp.Auditing.Dto;
using PearAdmin.Abp.CommonDto;

namespace PearAdmin.Abp.Auditing
{
    public interface IAuditLogAppService : IApplicationService
    {
        /// <summary>
        /// 分页、筛选审计日志列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<PagedResultDto<AuditLogListDto>> GetAuditLogList(GetPagedAuditLogsInput input);

        Task<FileDto> GetAuditLogsToExcel(GetPagedAuditLogsInput input);

        Task<PagedResultDto<EntityChangeListDto>> GetEntityChanges(GetEntityChangeInput input);

        Task<PagedResultDto<EntityChangeListDto>> GetEntityTypeChanges(GetEntityTypeChangeInput input);

        Task<FileDto> GetEntityChangesToExcel(GetEntityChangeInput input);

        Task<List<EntityPropertyChangeDto>> GetEntityPropertyChanges(long entityChangeId);

        List<NameValueDto> GetEntityHistoryObjectTypes();
    }
}