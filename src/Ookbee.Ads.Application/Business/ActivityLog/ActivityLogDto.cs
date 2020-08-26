using Ookbee.Ads.Application.Business.User;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.ActivityLog
{
    public class ActivityLogDto : DefaultDto
    {
        public long ObjectId { get; set; }
        public string ObjectType { get; set; }
        public string ObjectData { get; set; }
        public LogEvent Activity { get; set; }
        public UserDto User { get; set; }

        public static Expression<Func<ActivityLogEntity, ActivityLogDto>> Projection
        {
            get
            {
                return entity => new ActivityLogDto()
                {
                    Id = entity.Id,
                    ObjectId = entity.Id,
                    ObjectType = entity.ObjectType,
                    ObjectData = entity.ObjectData,
                    Activity = entity.Activity,
                    User = new UserDto()
                    {
                        Id = entity.User.Id,
                        UserName = entity.User.UserName,
                        DisplayName = entity.User.DisplayName,
                        AvatarUrl = entity.User.AvatarUrl,
                    }
                };
            }
        }
    }
}
