using System;
using System.IO;
using Abp.Reflection.Extensions;

namespace PearAdmin.AbpTemplate
{
    public class AbpTemplateCoreConsts
    {
        public const string Version = "5.5.0.0";
        public static DateTime ReleaseDate => LzyReleaseDate.Value;
        private static readonly Lazy<DateTime> LzyReleaseDate = new Lazy<DateTime>(() => new FileInfo(typeof(AbpTemplateCoreConsts).GetAssembly().Location).LastWriteTime);

        public const string LocalizationSourceName = "zh-Hans";
        public const string ConnectionStringName = "Default";
        public const string RedisConnectionStringName = "Redis";
        public const string DefaultCurrentEnviroment = "Development";
        public const bool MultiTenancyEnabled = true;
        public const bool AllowTenantsToChangeEmailSettings = false;
    }
}
