using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Common
{
    public class DefaultSettingsCreator
    {
        private readonly AbpTemplateDbContext _context;
        private readonly int? _tenantId;

        public DefaultSettingsCreator(AbpTemplateDbContext context, int? tenantId = null)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateDefaultSetting();
        }

        private void CreateDefaultSetting()
        {
            // 邮箱设置
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "admin@mydomain.com", _tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "mydomain.com mailer", _tenantId);

            // 语言设置
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "zh-Hans", _tenantId);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}
