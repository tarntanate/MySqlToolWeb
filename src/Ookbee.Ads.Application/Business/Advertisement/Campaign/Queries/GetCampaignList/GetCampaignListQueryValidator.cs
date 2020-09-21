﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.IsExistsAdvertiserById;
using Ookbee.Ads.Infrastructure.Models;
using System;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Queries.GetCampaignList
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
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdvertiserByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.Ok)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });
        }
    }
}