﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById
{
    public class IsExistsAdvertiserByIdQueryValidator : AbstractValidator<IsExistsAdvertiserByIdQuery>
    {
        public IsExistsAdvertiserByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
