using System;
using MediatR;
using Microsoft.DotNet.PlatformAbstractions;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.UpdateAdGroupStat
{
    public class UpdateAdGroupStatCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        public long AdGroupId { get; set; }
        public Platform Platform { get; set; }
        public long Request { get; set; }
        public DateTime CaculatedAt { get; set; }

        public UpdateAdGroupStatCommand(long id, long adGroupId, Platform platform, long request, DateTime caculatedAt)
        {
            Id = id;
            AdGroupId = adGroupId;
            Platform = platform;
            Request = request;
            CaculatedAt = caculatedAt;
        }
    }
}
