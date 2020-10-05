﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Services.Analytics.AdGroupStat.Commands.CreateAdGroupStats
{
    public class CreateAdGroupStatsCommandValidator : AbstractValidator<CreateAdGroupStatsCommand>
    {
        public IMediator Mediator { get; set; }

        public CreateAdGroupStatsCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdGroupById = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!isExistsAdGroupById.IsSuccess)
                        context.AddFailure(isExistsAdGroupById.Message);
                });
        }
    }
}