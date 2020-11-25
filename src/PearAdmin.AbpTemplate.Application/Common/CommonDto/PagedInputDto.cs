using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PearAdmin.AbpTemplate.CommonDto
{
    /// <summary>
    /// 分页Dto
    /// </summary>
    public class PagedInputDto : IPagedResultRequest
    {
        public PagedInputDto()
        {
            MaxResultCount = AbpTemplateApplicationConsts.DefaultPageSize;
        }

        /// <summary>
        /// 页面最大记录数
        /// </summary>
        [Range(1, AbpTemplateApplicationConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        /// <summary>
        /// 跳过记录数
        /// </summary>
        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }
    }
}
