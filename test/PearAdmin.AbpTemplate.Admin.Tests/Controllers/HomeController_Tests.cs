using System.Threading.Tasks;
using PearAdmin.AbpTemplate.Admin.Controllers;
using Shouldly;
using Xunit;

namespace PearAdmin.AbpTemplate.Admin.Tests.Controllers
{
    public class HomeController_Tests: AbpTemplateWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            LoginAsDefaultTenantAdmin();

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}