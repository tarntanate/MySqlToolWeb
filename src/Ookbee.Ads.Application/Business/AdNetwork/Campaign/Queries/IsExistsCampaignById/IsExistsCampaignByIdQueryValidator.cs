﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.IsExistsCampaignById
{
    public class IsExistsCampaignByIdQueryValidator : AbstractValidator<IsExistsCampaignByIdQuery>
    {
        public IsExistsCampaignByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}