using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.Abp.CommonDto
{
    /// <summary>
    /// 分页Dto
    /// </summary>
    public class PagedInputDto : IPagedResultRequest
    {
        public PagedInputDto()
        {
            MaxResultCount = AbpApplicationConsts.DefaultPageSize;
        }

        /// <summary>
        /// 页面最大记录数
        /// </summary>
        [Range(1, AbpApplicationConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 跳过记录数
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
    }
}
