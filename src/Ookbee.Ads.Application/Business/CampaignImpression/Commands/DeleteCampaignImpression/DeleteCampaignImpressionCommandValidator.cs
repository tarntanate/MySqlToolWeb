﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.DeleteCampaignImpression
{
    public class DeleteCampaignImpressionCommandValidator : AbstractValidator<DeleteCampaignImpressionCommand>
    {
        public DeleteCampaignImpressionCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
