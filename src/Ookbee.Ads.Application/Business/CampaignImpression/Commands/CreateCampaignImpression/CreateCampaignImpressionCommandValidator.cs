﻿using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Common;
using System.Threading.Tasks;
using System.Threading;

namespace Ookbee.Ads.Application.Business.CampaignImpression.Commands.CreateCampaignImpression
{
    public class CreateCampaignImpressionCommandValidator : AbstractValidator<CreateCampaignImpressionCommand>
    {
        private IMediator Mediator { get; }

        public CreateCampaignImpressionCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.StartDate)
                .GreaterThanOrEqualTo(MechineDateTime.Now)
                .LessThan(p => p.EndDate);

            RuleFor(p => p.EndDate)
                .GreaterThanOrEqualTo(MechineDateTime.Now)
                .GreaterThan(p => p.StartDate);

            RuleFor(p => p.Qouta)
                .GreaterThan(0);

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");

            RuleFor(p => p.AdvertiserId)
                .CustomAsync(BeAValidAdvertiserId);
        }

        private async Task BeAValidAdvertiserId(long value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value));
            if (!result.Ok)
                context.AddFailure(result.Message);
        }
    }
}
