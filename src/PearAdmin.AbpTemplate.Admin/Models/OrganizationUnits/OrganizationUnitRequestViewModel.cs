using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Admin.Models.OrganizationUnits
{
    /// <summary>
    /// 组织机构请求参数视图模型
    /// </summary>
    public class OrganizationUnitRequestViewModel
    {
        /// <summary>
        /// 目标组织机构Id
        /// </summary>
        public long? Id { get; set; }

        /// <summary>
        /// 组织机构父级Id
        /// </summary>
        public long? ParentId { get; set; }
    }
}
