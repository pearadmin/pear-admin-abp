using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate.Localization
{
    public static class AbpTemplateLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(AbpTemplateCoreConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(AbpTemplateLocalizationConfigurer).GetAssembly(),
                        "PearAdmin.AbpTemplate.Common.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
