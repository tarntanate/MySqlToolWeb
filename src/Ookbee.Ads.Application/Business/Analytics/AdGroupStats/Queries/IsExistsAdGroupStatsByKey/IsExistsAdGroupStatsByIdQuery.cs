﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Queries.IsExistsAdGroupStatsByKey
{
    public class IsExistsAdGroupStatsByKeyQuery : IRequest<Response<bool>>
    {
        public long AdGroupId { get; set; }
        public DateTimeOffset CaculatedAt { get; set; }

        public IsExistsAdGroupStatsByKeyQuery(long adGroupId, DateTimeOffset caculatedAt)
        {
            AdGroupId = adGroupId;
            CaculatedAt = caculatedAt;
        }
    }
}
