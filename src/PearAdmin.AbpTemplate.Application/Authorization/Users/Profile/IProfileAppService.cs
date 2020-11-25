using Abp.Application.Services.Dto;
using PearAdmin.AbpTemplate.Authorization.Users.Profile.Dto;
using System.Threading.Tasks;

namespace PearAdmin.AbpTemplate.Authorization.Users.Profile
{
    public interface IProfileAppService
    {
        Task ChangePhoneNumber(ChangePhoneNumberDto input);

        Task ChangePassword(ChangePasswordDto input);

        Task<GetProfilePictureOutput> GetProfilePicture();

        Task<GetProfilePictureOutput> GetProfilePictureByUser(EntityDto<long> input);

        Task UpdateProfilePicture(UpdateProfilePictureInput input);
    }
}
