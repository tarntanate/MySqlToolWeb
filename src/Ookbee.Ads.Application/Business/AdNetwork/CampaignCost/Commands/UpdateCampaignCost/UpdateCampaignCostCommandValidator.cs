﻿using FluentValidation;
using MediatR;

namespace Ookbee.Ads.Application.Business.AdNetwork.CampaignCost.Commands.UpdateCampaignCost
{
    public class UpdateCampaignCostCommandValidator : AbstractValidator<UpdateCampaignCostCommand>
    {
        private IMediator Mediator { get; }

        public UpdateCampaignCostCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Budget)
                .GreaterThan(0);

            RuleFor(p => p.CostPerUnit)
                .GreaterThan(0);
        }
    }
}