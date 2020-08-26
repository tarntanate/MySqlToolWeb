﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdStatsList.Queries.GetAdStatsList
{
    public class GetAdStatsListQueryValidator : AbstractValidator<GetAdStatsListQuery>
    {
        public GetAdStatsListQueryValidator()
        {
            RuleFor(p => p.AdId).GreaterThan(0);
        }
    }
}