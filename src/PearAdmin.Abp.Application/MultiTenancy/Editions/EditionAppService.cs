using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp;
using Abp.Application.Editions;
using Abp.Application.Features;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.BackgroundJobs;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Abp.UI;
using PearAdmin.Abp.Authorization;
using PearAdmin.Abp.Editions;
using PearAdmin.Abp.MultiTenancy.Editions.Dto;

namespace PearAdmin.Abp.MultiTenancy.Editions
{
    /// <summary>
    /// 版本管理应用服务实现
    /// </summary>
    public class EditionAppService : AbpApplicationServiceBase, IEditionAppService
    {
        #region 初始化
        private readonly EditionManager _editionManager;
        private readonly IRepository<Edition> _editionRepository;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public EditionAppService(
            EditionManager editionManager,
            IRepository<Edition> editionRepository,
            IRepository<Tenant> tenantRepository,
            IBackgroundJobManager backgroundJobManager)
        {
            _editionManager = editionManager;
            _editionRepository = editionRepository;
            _tenantRepository = tenantRepository;
            _backgroundJobManager = backgroundJobManager;
        }
        #endregion

        #region 版本管理
        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Editions)]
        public async Task<ListResultDto<EditionDto>> GetAllEdition()
        {
            var editions = await _editionRepository.GetAllListAsync();

            return new ListResultDto<EditionDto>(
                editions.Select(item =>
                {
                    var dto = ObjectMapper.Map<EditionDto>(item);
                    dto.LastModificationTime = item.LastModificationTime.HasValue ? item.LastModificationTime.Value : item.CreationTime;
                    return dto;
                }).ToList());
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Editions_Create, AppPermissionNames.Pages_SystemManagement_Editions_Update)]
        public async Task<EditionEditDto> GetEditionForEdit(NullableIdDto input)
        {
            var editionEditDto = new EditionEditDto()
            {
                Features = new List<EditionFeatureDto>()
            };

            editionEditDto.Features = FeatureManager.GetAll()
                .Where(f => f.Scope.HasFlag(FeatureScopes.Edition))
                .Select(f => new EditionFeatureDto()
                {
                    Id = f.Name,
                    ParentId = f.Parent != null ? f.Parent.Name : string.Empty,
                    Name = f.Name,
                    ParentName = f.Parent != null ? f.Parent.Name : string.Empty,
                    DisplayName = f.Name
                })
                .ToList();

            if (input.Id.HasValue)
            {
                var edition = await _editionManager.FindByIdAsync(input.Id.Value);
                editionEditDto.Id = edition.Id;
                editionEditDto.DisplayName = edition.DisplayName;
                var grantedFeatureValues = (await _editionManager.GetFeatureValuesAsync(input.Id.Value)).ToList();

                foreach (var editionFeatureDto in editionEditDto.Features)
                {
                    var grantedFeatureValue = grantedFeatureValues
                        .Where(g => g.Name == editionFeatureDto.Name)
                        .FirstOrDefault();

                    if (grantedFeatureValue != null)
                    {
                        editionFeatureDto.IsAssigned = bool.Parse(grantedFeatureValue.Value);
                    }
                }
            }

            return editionEditDto;
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Editions_Create)]
        public async Task CreateEdition(CreateEditionDto input)
        {
            input.FeatureValues = ValidateFeatures(input.FeatureValues);

            var edition = new Edition()
            {
                Name = input.DisplayName,
                DisplayName = input.DisplayName
            };

            await _editionManager.CreateAsync(edition);
            await CurrentUnitOfWork.SaveChangesAsync();

            await SetFeatureValues(edition, input.FeatureValues);
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Editions_Update)]
        public async Task UpdateEdition(UpdateEditionDto input)
        {
            input.FeatureValues = ValidateFeatures(input.FeatureValues);

            var edition = await _editionManager.GetByIdAsync(input.Id);
            edition.Name = input.DisplayName;
            edition.DisplayName = input.DisplayName;

            await SetFeatureValues(edition, input.FeatureValues);
        }

        [AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Editions_Delete)]
        public async Task DeleteEdition(EntityDto input)
        {
            var tenantCount = await _tenantRepository.CountAsync(t => t.EditionId == input.Id);
            if (tenantCount > 0)
            {
                throw new UserFriendlyException(L("ThereAreTenantsSubscribedToThisEdition"));
            }

            var edition = await _editionManager.GetByIdAsync(input.Id);
            await _editionManager.DeleteAsync(edition);
        }

        //[AbpAuthorize(AppPermissionNames.Pages_SystemManagement_Editions_MoveTenantsToAnotherEdition)]
        public async Task MoveTenantsToAnotherEdition(MoveTenantsToAnotherEditionDto input)
        {
            await _backgroundJobManager.EnqueueAsync<MoveTenantsToAnotherEditionJob, MoveTenantsToAnotherEditionJobArgs>(new MoveTenantsToAnotherEditionJobArgs
            {
                SourceEditionId = input.SourceEditionId,
                TargetEditionId = input.TargetEditionId,
                User = AbpSession.ToUserIdentifier()
            });
        }
        #endregion

        #region 辅助方法
        private List<NameValueDto> ValidateFeatures(List<NameValueDto> featureValues)
        {
            var allFeatureValues = FeatureManager.GetAll()
                .Where(f => f.Scope.HasFlag(FeatureScopes.Edition))
                .Select(f => new NameValue(f.Name, f.DefaultValue))
                .ToList();

            foreach (var featureValue in allFeatureValues)
            {
                foreach (var item in featureValues)
                {
                    if (featureValue.Name == item.Name)
                    {
                        featureValue.Value = item.Value;
                        break;
                    }
                }
            }

            return allFeatureValues.Select(fv => new NameValueDto(fv)).ToList();
        }

        private Task SetFeatureValues(Edition edition, List<NameValueDto> featureValues)
        {
            return _editionManager.SetFeatureValuesAsync(edition.Id, featureValues.Select(fv => new NameValue(fv.Name, fv.Value)).ToArray());
        }
        #endregion
    }
}
