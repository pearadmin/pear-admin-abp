using System.Linq;
using Abp.Configuration;
using Microsoft.EntityFrameworkCore;
using PearAdmin.AbpTemplate.AppProvider.Configuration.MailboxTemplates;
using PearAdmin.AbpTemplate.Configuration;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Common
{
    public class TenantSettingsCreator
    {
        private readonly AbpTemplateDbContext _context;
        private readonly int? _tenantId;

        public TenantSettingsCreator(AbpTemplateDbContext context, int? tenantId = null)
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
            // 邀请模板设置
            AddSettingIfNotExists(AppSettingNames.TenantManagement.InviteMailboxTemplate, InviteMailboxTemplate.DefaultTemplate(), _tenantId);
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
