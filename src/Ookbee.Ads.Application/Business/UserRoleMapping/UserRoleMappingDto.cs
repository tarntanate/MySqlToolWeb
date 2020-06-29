using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.UserRoleMapping
{
    public class UserRoleMappingDto
    {
        public long UserId { get; set; }
        public long RoleId { get; set; }

        public static Expression<Func<UserRoleMappingEntity, UserRoleMappingDto>> Projection
        {
            get
            {
                return entity => new UserRoleMappingDto()
                {
                    UserId = entity.UserId,
                    RoleId = entity.RoleId,
                };
            }
        }
    }
}
