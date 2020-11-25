using Abp.Application.Features;
using Abp.Localization;
using Abp.UI.Inputs;

namespace PearAdmin.Abp.Features
{
    public class AppFeatureProvider : FeatureProvider
    {
        public override void SetFeatures(IFeatureDefinitionContext context)
        {
            context.Create(
                AppFeatureNames.HostSettings,
                defaultValue: "false",
                displayName: L("HostSettings"),
                inputType: new CheckboxInputType()
            );

            context.Create(
                AppFeatureNames.ChatFeature,
                defaultValue: "false",
                displayName: L("ChatFeature"),
                inputType: new CheckboxInputType()
            );

            context.Create(
                AppFeatureNames.TenantToTenantChatFeature,
                defaultValue: "false",
                displayName: L("TenantToTenantChatFeature"),
                inputType: new CheckboxInputType()
            );

            context.Create(
                AppFeatureNames.TenantToHostChatFeature,
                defaultValue: "false",
                displayName: L("TenantToHostChatFeature"),
                inputType: new CheckboxInputType()
            );
        }

        private ILocalizableString L(string name)
        {
            return new LocalizableString(name, AbpCoreConsts.LocalizationSourceName);
        }
    }
}
