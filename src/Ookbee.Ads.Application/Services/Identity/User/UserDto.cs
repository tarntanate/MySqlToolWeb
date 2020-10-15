using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Identity.UserRole;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Identity.User
{
    public class UserDto : DefaultDto
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string AvatarUrl { get; set; }
        public UserRoleEntity Role { get; set; }

        public static Expression<Func<UserEntity, UserDto>> Projection
        {
            get
            {
                return entity => new UserDto()
                {
                    Id = entity.Id,
                    UserName = entity.UserName,
                    DisplayName = entity.DisplayName,
                    AvatarUrl = entity.AvatarUrl,
                    Role = entity.Role
                };
            }
        }
    }
}
