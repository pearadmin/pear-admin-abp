using Abp.Application.Services.Dto;

namespace PearAdmin.Abp.CommonDto
{
    /// <summary>
    /// 分页及排序Dto
    /// </summary>
    public class PagedAndSortedInputDto : PagedInputDto, ISortedResultRequest
    {
        public PagedAndSortedInputDto()
        {
            MaxResultCount = AbpApplicationConsts.DefaultPageSize;
        }

        public string Sorting { get; set; }
    }
}
