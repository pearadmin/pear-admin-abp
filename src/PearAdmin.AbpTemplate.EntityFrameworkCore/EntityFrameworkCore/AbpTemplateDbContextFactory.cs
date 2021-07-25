using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PearAdmin.AbpTemplate.Configuration;
using PearAdmin.AbpTemplate.Web;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore
{
    /// <summary>
    /// 只在用命令构建迁移脚本时使用
    /// https://docs.microsoft.com/zh-cn/ef/core/cli/dbcontext-creation?tabs=vs
    /// </summary>
    public class AbpTemplateDbContextFactory : IDesignTimeDbContextFactory<AbpTemplateDbContext>
    {
        public AbpTemplateDbContext CreateDbContext(string[] args)
        {
            var currentEnvironment = GetCurrentEnvironment(args);
            var builder = new DbContextOptionsBuilder<AbpTemplateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), currentEnvironment);
            AbpTemplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AbpTemplateCoreConsts.ConnectionStringName));

            return new AbpTemplateDbContext(builder.Options);
        }

        public string GetCurrentEnvironment(string[] args)
        {
            return AbpTemplateCoreConsts.DefaultCurrentEnviroment;
        }
    }
}
