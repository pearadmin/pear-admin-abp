using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore
{
    public static class AbpTemplateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AbpTemplateDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion);
        }

        public static void Configure(DbContextOptionsBuilder<AbpTemplateDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection, MySqlServerVersion.LatestSupportedServerVersion);
        }
    }
}
