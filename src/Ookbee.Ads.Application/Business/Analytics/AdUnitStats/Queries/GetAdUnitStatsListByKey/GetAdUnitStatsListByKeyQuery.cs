﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Analytics.AdUnitStats.Queries.GetAdUnitStatsListByKey
{
    public class GetAdUnitStatsListByKeyQuery : IRequest<HttpResult<IEnumerable<AdUnitStatsDto>>>
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public long? AdUnitId { get; set; }
        public DateTimeOffset? CaculatedAt { get; set; }

        public GetAdUnitStatsListByKeyQuery(int start, int length, long? adUnitId, DateTimeOffset? caculatedAt)
        {
            Start = start;
            Length = length;
            AdUnitId = adUnitId;
            CaculatedAt = caculatedAt;
        }
    }
}