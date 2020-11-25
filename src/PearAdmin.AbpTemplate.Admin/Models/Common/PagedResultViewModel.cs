using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Admin.Models.Common
{
    /// <summary>
    /// 分页结果视图模型
    /// </summary>
    public class PagedResultViewModel<T> : ResponseParamViewModel
    {
        public PagedResultViewModel(int count, IReadOnlyList<T> data, string msg = "", int code = 200)
            : base(msg, code)
        {
            Count = count;
            Data = data;
        }

        public int Count { get; set; }

        public IReadOnlyList<T> Data { get; set; }
    }
}
