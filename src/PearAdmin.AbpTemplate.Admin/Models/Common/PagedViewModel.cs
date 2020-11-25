using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 分页请求视图模型
    /// </summary>
    public class PagedViewModel
    {
        public int Page { get; set; }

        public int Limit { get; set; } = 10;
    }
}
