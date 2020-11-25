using Abp.Reflection.Extensions;
using System;
using System.IO;

namespace PearAdmin.AbpTemplate
{
    public class AbpTemplateCoreConsts
    {
        /// <summary>
        /// Gets current version of the application.
        /// It's also shown in the web page.
        /// </summary>
        public const string Version = "5.5.0.0";

        /// <summary>
        /// Gets release (last build) date of the application.
        /// It's shown in the web page.
        /// </summary>
        public static DateTime ReleaseDate => LzyReleaseDate.Value;

        private static readonly Lazy<DateTime> LzyReleaseDate = new Lazy<DateTime>(() => new FileInfo(typeof(AbpTemplateCoreConsts).GetAssembly().Location).LastWriteTime);

        public const string LocalizationSourceName = "zh-Hans";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;

        public const bool AllowTenantsToChangeEmailSettings = false;
    }
}
