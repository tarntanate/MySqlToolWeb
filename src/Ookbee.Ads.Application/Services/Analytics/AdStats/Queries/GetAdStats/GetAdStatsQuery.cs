﻿using MediatR;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdStats
{
    public class GetAdStatsQuery : IRequest<Response<AdStatsDto>>
    {
        public DateTimeOffset CaculatedAt { get; set; }
        public long AdId { get; set; }

        public GetAdStatsQuery(DateTimeOffset caculatedAt, long adId)
        {
            CaculatedAt = caculatedAt;
            AdId = adId;
        }
    }
}
