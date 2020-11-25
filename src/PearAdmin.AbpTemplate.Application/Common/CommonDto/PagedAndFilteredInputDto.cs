using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 分页及筛选Dto
    /// </summary>
    public class PagedAndFilteredInputDto : PagedInputDto, IPagedResultRequest
    {
        public PagedAndFilteredInputDto()
        {
            MaxResultCount = AbpTemplateApplicationConsts.DefaultPageSize;
        }

        public string FilterText { get; set; }
    }
}
