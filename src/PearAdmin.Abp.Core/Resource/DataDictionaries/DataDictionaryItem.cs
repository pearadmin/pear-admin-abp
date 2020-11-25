using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PearAdmin.Abp.Resource.DataDictionaries
{
    /// <summary>
    /// 资源_数据字典项
    /// </summary>
    [Table("Resource_DataDictionaryItem")]
    public class DataDictionaryItem : Entity<int>, IMustHaveTenant
    {
        public const int MaxCodeLength = 5;
        public const int MaxNameLength = 30;

        public DataDictionaryItem()
        {

        }

        public DataDictionaryItem(int tenantId, int dataDictionaryId)
        {
            TenantId = tenantId;
            DataDictionaryId = dataDictionaryId;
        }

        public static DataDictionaryItem Builder(int tenantId, int dataDictionaryId)
        {
            return new DataDictionaryItem(tenantId, dataDictionaryId);
        }

        public DataDictionaryItem SetNameAndCode(string name, string code)
        {
            Code = code;
            Name = name;
            return this;
        }

        /// <summary>
        /// 租户Id
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// 业务代码
        /// </summary>
        [StringLength(MaxCodeLength)]
        public string Code { get; private set; }

        /// <summary>
        /// 类型项名称
        /// </summary>
        [StringLength(MaxNameLength)]
        public string Name { get; private set; }

        /// <summary>
        /// 数据字典Id
        /// </summary>
        public int DataDictionaryId { get; private set; }
    }
}
