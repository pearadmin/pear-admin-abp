using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace PearAdmin.Abp.EntityFrameworkCore
{
    public static class AbpDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AbpDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AbpDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
