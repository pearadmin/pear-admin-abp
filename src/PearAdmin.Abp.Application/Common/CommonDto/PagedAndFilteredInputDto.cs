using Abp.Application.Services.Dto;

namespace PearAdmin.Abp.CommonDto
{
    /// <summary>
    /// 分页及筛选Dto
    /// </summary>
    public class PagedAndFilteredInputDto : PagedInputDto, IPagedResultRequest
    {
        public PagedAndFilteredInputDto()
        {
            MaxResultCount = AbpApplicationConsts.DefaultPageSize;
        }

        public string FilterText { get; set; }
    }
}
