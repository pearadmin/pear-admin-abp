using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using Microsoft.EntityFrameworkCore;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Seed.Common
{
    public class DefaultLanguagesCreator
    {
        private readonly AbpTemplateDbContext _context;
        private readonly int? _tenantId;

        public DefaultLanguagesCreator(AbpTemplateDbContext context, int? tenantId = null)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            CreateDefaultLanguage();
        }

        private void CreateDefaultLanguage()
        {
            var initialLanguages = new List<ApplicationLanguage>
            {
                new ApplicationLanguage(_tenantId, "zh-Hans", "简体中文", "famfamfam-flags cn"),
                new ApplicationLanguage(_tenantId, "en", "English", "famfamfam-flags us")
            };

            foreach (var language in initialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);
            _context.SaveChanges();
        }
    }
}
