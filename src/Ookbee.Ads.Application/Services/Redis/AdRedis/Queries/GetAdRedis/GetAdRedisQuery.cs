using System;
using MediatR;
using Ookbee.Ads.Common.Helpers;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Cache.AdRedis.Commands.GetAdRedis
{
    public class GetAdRedisQuery : IRequest<Response<string>>
    {
        public AdPlatform Platform { get; private set; }
        public long AdUnitId { get; private set; }
        public long? UserId => Int64.TryParse(UserIdText, out long number) ? number : default(long?);
        public string UserIdText { get; private set; }

        public GetAdRedisQuery(string platform, long adUnitId, string userIdText)
        {
            Platform = EnumHelper.ConvertTo<AdPlatform>(platform);
            AdUnitId = adUnitId;
            UserIdText = userIdText;
        }
    }
}
