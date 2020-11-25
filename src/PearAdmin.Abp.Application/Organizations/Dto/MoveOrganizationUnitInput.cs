using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PearAdmin.Abp.Organizations.Dto
{
    /// <summary>
    /// 迁移目标组织机构Dto
    /// </summary>
    public class MoveOrganizationUnitInput
    {
        [Range(1, long.MaxValue)]
        public long Id { get; set; }

        public long? NewParentId { get; set; }
    }
}
