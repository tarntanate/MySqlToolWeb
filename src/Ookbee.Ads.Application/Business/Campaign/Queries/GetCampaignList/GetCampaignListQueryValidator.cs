using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Enums;
using System;
using System.Threading;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQueryValidator : AbstractValidator<GetCampaignListQuery>
    {
        private IMediator Mediator { get; }

        public GetCampaignListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdvertiserId)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.PricingModel)
                .Must(BeValidPricingModel)
                .When(m => m.PricingModel.HasValue())
                .WithMessage("Only 'CPM' and 'IMP' Pricing Model is supported.");

            RuleFor(p => p.AdvertiserId)
                .CustomAsync(BeValidAdvertiserId);
        }

        private async System.Threading.Tasks.Task BeValidAdvertiserId(long? value, CustomContext context, CancellationToken cancellationToken)
        {
            if (value != null)
            {
                var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value.Value));
                if (!isExistsAdUnitResult.Ok)
                    context.AddFailure(isExistsAdUnitResult.Message);
            }

        }

        private bool BeValidPricingModel(string value)
        {
            if (Enum.TryParse<PricingModel>(value, true, out var pricingModel))
                return true;
            return false;
        }
    }
}
