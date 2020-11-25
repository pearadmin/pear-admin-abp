using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using PearAdmin.AbpTemplate.Configuration;
using PearAdmin.AbpTemplate.Web;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class AbpTemplateDbContextFactory : IDesignTimeDbContextFactory<AbpTemplateDbContext>
    {
        public AbpTemplateDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AbpTemplateDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            AbpTemplateDbContextConfigurer.Configure(builder, configuration.GetConnectionString(AbpTemplateCoreConsts.ConnectionStringName));

            return new AbpTemplateDbContext(builder.Options);
        }
    }
}
