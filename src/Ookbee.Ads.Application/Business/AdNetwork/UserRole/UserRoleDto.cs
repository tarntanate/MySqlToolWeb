using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRole
{
    public class UserRoleDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public static Expression<Func<UserRoleEntity, UserRoleDto>> Projection
        {
            get
            {
                return entity => new UserRoleDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                };
            }
        }
    }
}
