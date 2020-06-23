﻿using FluentValidation;
using MediatR;
using System.Threading;

namespace Ookbee.Ads.Application.Business.CampaignCost.Commands.CreateCampaignCost
{
    public class CreateCampaignCostCommandValidator : AbstractValidator<CreateCampaignCostCommand>
    {
        private IMediator Mediator { get; }

        public CreateCampaignCostCommandValidator(IMediator mediator)
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
