using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Redis.AdUserRedis.Commands.IsExistsAdUserPreviewRedis
{
    public class IsExistsAdUserPreviewRedisQuery : IRequest<Response<bool>>
    {
        public long UserId { get; set; }

        public IsExistsAdUserPreviewRedisQuery(long userId)
        {
            UserId = userId;
        }
    }
}
