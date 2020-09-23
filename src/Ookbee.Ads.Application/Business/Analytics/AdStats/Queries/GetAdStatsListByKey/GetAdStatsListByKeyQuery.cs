﻿using MediatR;
using Ookbee.Ads.Application.Business.Analytics.AdStats;
using Ookbee.Ads.Common.Response;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Analytics.AdStat.Queries.GetAdStatsListByKey
{
    public class GetAdStatsListByKeyQuery : IRequest<Response<IEnumerable<AdStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdId { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }

        public GetAdStatsListByKeyQuery(int start, int length, long? adId, DateTimeOffset? caculatedAt)
        {
            Start = start;
            Length = length;
            AdId = adId;
            CaculatedAt = caculatedAt;
        }
    }
}
