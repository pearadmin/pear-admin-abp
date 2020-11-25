using Abp.Application.Services.Dto;
using Abp.AutoMapper;

namespace PearAdmin.Abp.Admin.Models.Common
{
    /// <summary>
    /// 基础实体视图模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [AutoMapTo(typeof(EntityDto))]
    public class EntityViewModel<T>
    {
        public EntityViewModel()
        {

        }

        public EntityViewModel(T _Id)
        {
            Id = Id;
        }

        public T Id { get; set; }
    }
}
