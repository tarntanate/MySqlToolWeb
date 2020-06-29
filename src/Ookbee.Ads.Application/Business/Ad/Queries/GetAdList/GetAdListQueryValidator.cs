﻿using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsCampaignById;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdList
{
    public class GetAdListQueryValidator : AbstractValidator<GetAdListQuery>
    {
        private IMediator Mediator { get; }

        public GetAdListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.Ok)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });

            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsCampaignResult = await Mediator.Send(new IsExistsCampaignByIdQuery(value.Value), cancellationToken);
                        if (!isExistsCampaignResult.Ok)
                            context.AddFailure(isExistsCampaignResult.Message);
                    }
                });
        }
    }
}
