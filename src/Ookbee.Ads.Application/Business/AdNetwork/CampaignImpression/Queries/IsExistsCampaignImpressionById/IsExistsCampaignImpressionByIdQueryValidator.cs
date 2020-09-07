﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignImpression.Queries.IsExistsCampaignImpressionById
{
    public class IsExistsCampaignImpressionByIdQueryValidator : AbstractValidator<IsExistsCampaignImpressionByIdQuery>
    {
        public IsExistsCampaignImpressionByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}