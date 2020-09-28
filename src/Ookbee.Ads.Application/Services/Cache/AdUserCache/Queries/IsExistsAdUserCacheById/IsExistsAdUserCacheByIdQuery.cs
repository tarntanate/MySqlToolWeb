using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Cache.AdUserCache.Queries.IsExistsAdUserCacheById
{
    public class IsExistsAdUserCacheByIdQuery : IRequest<Response<bool>>
    {
        public long UserId { get; set; }

        public IsExistsAdUserCacheByIdQuery(long userId)
        {
            UserId = userId;
        }
    }
}
