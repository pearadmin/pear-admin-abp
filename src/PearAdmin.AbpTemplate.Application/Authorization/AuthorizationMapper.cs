using Abp.Authorization;
using AutoMapper;
using PearAdmin.AbpTemplate.Authorization.Permissions.Dto;
using PearAdmin.AbpTemplate.Authorization.Roles;
using PearAdmin.AbpTemplate.Authorization.Roles.Dto;
using PearAdmin.AbpTemplate.Authorization.Users;
using PearAdmin.AbpTemplate.Authorization.Users.Dto;

namespace PearAdmin.AbpTemplate.Authorization
{
    internal static class AuthorizationMapper
    {
        public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Role, RoleDto>();
            configuration.CreateMap<Permission, RolePermissionDto>();
            configuration.CreateMap<User, UserDto>();
            configuration.CreateMap<User, UserEditDto>();
            configuration.CreateMap<Permission, PermissionDto>();
        }
    }
}
