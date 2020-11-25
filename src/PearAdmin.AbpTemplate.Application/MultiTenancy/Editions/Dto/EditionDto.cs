using System;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;

namespace PearAdmin.AbpTemplate.MultiTenancy.Editions.Dto
{
    public class EditionDto : EntityDto, IHasModificationTime
    {
        public string Name { get; set; }

        public string DisplayName { get; set; }

        public DateTime? LastModificationTime { get; set; }
    }
}