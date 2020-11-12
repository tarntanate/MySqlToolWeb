﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsIdList
{
    public class GetAdGroupStatsIdListQueryValidator : AbstractValidator<GetAdGroupStatsIdListQuery>
    {
        public GetAdGroupStatsIdListQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
