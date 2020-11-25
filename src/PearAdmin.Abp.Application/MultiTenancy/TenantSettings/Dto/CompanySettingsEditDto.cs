using System;
using System.Collections.Generic;
using System.Text;

namespace PearAdmin.Abp.MultiTenancy.TenantSetting.Dto
{
    public class CompanySettingsEditDto
    {
        /// <summary>
        /// 公司名称
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { get; set; }
    }
}
