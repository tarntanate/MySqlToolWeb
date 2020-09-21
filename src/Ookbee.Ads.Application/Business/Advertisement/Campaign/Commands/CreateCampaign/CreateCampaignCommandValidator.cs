﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.GetCampaignByName;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Commands.CreateCampaign
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
        }
    }
}