using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Cache.Queries.GetAdGroupIdListCache
{
    public class GetAdGroupIdListCacheQuery : IRequest<Response<IEnumerable<long>>>
    {
        public GetAdGroupIdListCacheQuery()
        {
            
        }
    }
}
