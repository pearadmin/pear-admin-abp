using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Application.Services.Dto;

namespace PearAdmin.Abp.MultiTenancy.Editions.Dto
{
    public class CreateEditionDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        public List<NameValueDto> FeatureValues { get; set; }
    }
}