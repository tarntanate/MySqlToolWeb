﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.GetAdGroupIdList
{
    public class GetAdGroupIdListQueryValidator : AbstractValidator<GetAdGroupIdListQuery>
    {
        private readonly IMediator Mediator;

        public GetAdGroupIdListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
            RuleFor(p => p.AdUnitTypeId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdUnitResult.IsSuccess)
                            context.AddFailure(isExistsAdUnitResult.Message);
                    }
                });

            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsPublisherResult = await Mediator.Send(new IsExistsPublisherByIdQuery(value.Value), cancellationToken);
                        if (!isExistsPublisherResult.IsSuccess)
                            context.AddFailure(isExistsPublisherResult.Message);
                    }
                });
        }
    }
}
