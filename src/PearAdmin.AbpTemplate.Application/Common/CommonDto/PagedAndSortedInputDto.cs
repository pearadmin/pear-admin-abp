using Abp.Application.Services.Dto;

namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 分页及排序Dto
    /// </summary>
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public PagedAndSortedInputDto()
        {
            MaxResultCount = AbpTemplateApplicationConsts.DefaultPageSize;
        }

        public string Sorting { get; set; }
    }
}
