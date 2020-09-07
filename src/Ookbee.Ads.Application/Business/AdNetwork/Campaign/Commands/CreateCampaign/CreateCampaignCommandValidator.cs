﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.GetCampaignByName;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        private IMediator Mediator { get; }

        public CreateCampaignCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as CreateCampaignCommand;
                    var result = await Mediator.Send(new GetCampaignByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.PricingModel)
                .NotEmpty();

            RuleFor(p => p.StartDate)
                .GreaterThanOrEqualTo(MechineDateTime.Now.Date);

            RuleFor(p => p.EndDate)
                .GreaterThanOrEqualTo(MechineDateTime.Now.Date);
        }
    }
}