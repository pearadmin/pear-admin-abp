namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 分页、过滤及排序Dto
    /// </summary>
    public class PagedSortedAndFilteredInputDto : PagedAndSortedInputDto
    {
        public string FilterText { get; set; }
    }
}
