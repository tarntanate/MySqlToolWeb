﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.GetListCampaignAdvertiser
{
    public class GetListCampaignAdvertiserCommandValidator : AbstractValidator<GetListCampaignAdvertiserCommand>
    {
        public GetListCampaignAdvertiserCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
