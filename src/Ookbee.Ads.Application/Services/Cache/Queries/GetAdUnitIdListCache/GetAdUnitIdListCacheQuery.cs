using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.Queries.GetAdUnitIdListCache
{
    public class GetAdUnitIdListCacheQuery : IRequest<Response<IEnumerable<long>>>
    {
        public GetAdUnitIdListCacheQuery()
        {
            
        }
    }
}
