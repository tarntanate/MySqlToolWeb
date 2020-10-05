﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Advertiser.Queries.IsExistsAdvertiserById;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Queries.GetCampaignList
{
    public class GetCampaignListQueryValidator : AbstractValidator<GetCampaignListQuery>
    {
        private readonly IMediator Mediator;

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
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.IsSuccess)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });
        }
    }
}