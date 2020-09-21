using System;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Cache.AdUnitCache.Commands.GetAdUnitCacheByGroupId
{
    public class GetAdUnitCacheByGroupIdQuery : IRequest<HttpResult<string>>
    {
        public long AdGroupId { get; private set; }
        public Platform Platform { get; private set; }


        public GetAdUnitCacheByGroupIdQuery(long adGroupId, string platform)
        {
            AdGroupId = adGroupId;
            Platform = EnumHelper.ConvertTo<Platform>(platform);
        }
    }
}
