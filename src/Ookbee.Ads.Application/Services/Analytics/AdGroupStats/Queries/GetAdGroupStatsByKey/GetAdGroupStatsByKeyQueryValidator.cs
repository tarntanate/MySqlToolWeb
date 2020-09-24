﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Queries.GetAdGroupStatsByKey
{
    public class GetAdGroupStatsByKeyQueryValidator : AbstractValidator<GetAdGroupStatsByKeyQuery>
    {
        public GetAdGroupStatsByKeyQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
