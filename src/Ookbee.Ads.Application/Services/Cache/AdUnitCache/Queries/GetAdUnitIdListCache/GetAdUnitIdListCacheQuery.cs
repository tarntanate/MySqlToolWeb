using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.AdUnitCache.Queries.GetAdUnitIdListCache
{
    public class GetAdUnitIdListCacheQuery : IRequest<Response<IEnumerable<long>>>
    {
        public GetAdUnitIdListCacheQuery()
        {
            
        }
    }
}
