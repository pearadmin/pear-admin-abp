using Abp.Authorization;
using AutoMapper;
using PearAdmin.Abp.Authorization.Permissions.Dto;
using PearAdmin.Abp.Authorization.Roles;
using PearAdmin.Abp.Authorization.Roles.Dto;
using PearAdmin.Abp.Authorization.Users;
using PearAdmin.Abp.Authorization.Users.Dto;

namespace PearAdmin.Abp.Authorization
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
