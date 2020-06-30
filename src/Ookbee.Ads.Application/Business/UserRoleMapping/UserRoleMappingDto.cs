using System.Linq;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.User;
using Ookbee.Ads.Application.Business.UserRole;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.UserRoleMapping
{
    public class UserRoleMappingDto : UserDto
    {
        [JsonProperty(Order = 1)]
        public IEnumerable<UserRoleDto> Roles { get; set; }

        public new static Expression<Func<UserRoleMappingEntity, UserRoleMappingDto>> Projection
        {
            get
            {
                return entity => new UserRoleMappingDto()
                {
                    Id = entity.User.Id,
                    UserName = entity.User.UserName,
                    DisplayName = entity.User.DisplayName,
                    AvatarUrl = entity.User.AvatarUrl,
                    Roles = entity.User.UserRoleMappings.Select(m => new UserRoleDto()
                    {
                        Id = m.Role.Id,
                        Name = m.Role.Name,
                        Description = m.Role.Description,
                    })
                };
            }
        }
    }
}
