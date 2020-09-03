using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission
{
    public class UserPermissionDto : DefaultDto
    {
        public string ExtensionName { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsCreate { get; set; }
        public bool IsRead { get; set; }
        public bool IsUpdate { get; set; }
        public bool IsDelete { get; set; }

        public static Expression<Func<UserPermissionEntity, UserPermissionDto>> Projection
        {
            get
            {
                return entity => new UserPermissionDto()
                {
                    Id = entity.Id,
                    RoleId = entity.RoleId,
                    RoleName = entity.Role.Name,
                    ExtensionName = entity.ExtensionName,
                    IsCreate = entity.IsCreate,
                    IsRead = entity.IsRead,
                    IsUpdate = entity.IsUpdate,
                    IsDelete = entity.IsDelete,
                };
            }
        }
    }
}
