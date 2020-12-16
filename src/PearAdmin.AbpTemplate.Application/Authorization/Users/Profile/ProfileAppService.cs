using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.Auditing;
using Abp.Authorization;
using Abp.Runtime.Session;
using Abp.UI;
using PearAdmin.AbpTemplate.Authorization.Users.Profile.Dto;
using PearAdmin.AbpTemplate.BinaryObjects;

namespace PearAdmin.AbpTemplate.Authorization.Users.Profile
{
    [AbpAuthorize]
    public class ProfileAppService : AbpTemplateApplicationServiceBase, IProfileAppService
    {
        private const int MaxProfilPictureBytes = 5242880; //5MB
        private readonly IBinaryObjectManager _binaryObjectManager;

        public ProfileAppService(IBinaryObjectManager binaryObjectManager)
        {
            _binaryObjectManager = binaryObjectManager;
        }

        public async Task ChangePassword(ChangePasswordDto input)
        {
            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
            var user = await GetCurrentUserAsync();
            CheckErrors(await UserManager.ChangePasswordAsync(user, input.CurrentPassword, input.NewPassword));
        }

        public async Task ChangePhoneNumber(ChangePhoneNumberDto input)
        {
            await UserManager.InitializeOptionsAsync(AbpSession.TenantId);
            var user = await GetCurrentUserAsync();
            user.PhoneNumber = input.NewPhoneNumber;
            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [DisableAuditing]
        public async Task<GetProfilePictureOutput> GetProfilePicture()
        {
            return await GetProfilePicture(AbpSession.GetUserId());
        }

        [AbpAllowAnonymous]
        public async Task<GetProfilePictureOutput> GetProfilePictureByUser(EntityDto<long> input)
        {
            return await GetProfilePicture(input.Id);
        }

        public async Task UpdateProfilePicture(UpdateProfilePictureInput input)
        {
            byte[] byteArray;

            using (var bmpImage = new Bitmap(new MemoryStream(input.ImageBytes)))
            {
                using (var stream = new MemoryStream())
                {
                    bmpImage.Save(stream, bmpImage.RawFormat);
                    byteArray = stream.ToArray();
                }
            }

            if (byteArray.Length > MaxProfilPictureBytes)
            {
                throw new UserFriendlyException(L("ResizedProfilePicture_Warn_SizeLimit", AbpTemplateApplicationConsts.ResizedMaxProfilPictureBytesUserFriendlyValue));
            }

            var user = await UserManager.GetUserByIdAsync(AbpSession.GetUserId());

            if (user.ProfilePictureId.HasValue)
            {
                await _binaryObjectManager.DeleteAsync(user.ProfilePictureId.Value);
            }

            var storedFile = new BinaryObject(AbpSession.TenantId, byteArray);
            await _binaryObjectManager.SaveAsync(storedFile);

            user.ProfilePictureId = storedFile.Id;
        }

        private async Task<GetProfilePictureOutput> GetProfilePicture(long userId)
        {
            var profilePictureContent = string.Empty;
            var user = await UserManager.GetUserByIdAsync(userId);
            if (user.ProfilePictureId.HasValue)
            {
                var file = await _binaryObjectManager.GetOrNullAsync(user.ProfilePictureId.Value);
                if (file != null)
                {
                    profilePictureContent = Convert.ToBase64String(file.Bytes);
                }
            }

            return new GetProfilePictureOutput(profilePictureContent);
        }
    }
}
