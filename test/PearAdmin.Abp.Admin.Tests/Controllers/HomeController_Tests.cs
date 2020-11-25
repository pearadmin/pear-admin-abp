using System.Threading.Tasks;
using PearAdmin.Abp.Admin.Controllers;
using Shouldly;
using Xunit;

namespace PearAdmin.Abp.Admin.Tests.Controllers
{
    public class HomeController_Tests: AbpWebTestBase
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