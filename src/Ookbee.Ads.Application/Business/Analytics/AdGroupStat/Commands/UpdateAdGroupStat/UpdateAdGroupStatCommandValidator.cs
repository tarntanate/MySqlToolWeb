﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Business.Analytics.AdGroupStat.Commands.UpdateAdGroupStat
{
    public class UpdateAdGroupStatCommandValidator : AbstractValidator<UpdateAdGroupStatCommand>
    {
        public IMediator Mediator { get; set; }

        public UpdateAdGroupStatCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdGroupById = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!isExistsAdGroupById.Ok)
                        context.AddFailure(isExistsAdGroupById.Message);
                });

            RuleFor(p => p.Request)
                .GreaterThan(0);
        }
    }
}
