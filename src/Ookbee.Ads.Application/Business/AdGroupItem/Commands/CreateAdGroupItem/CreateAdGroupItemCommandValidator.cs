﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.IsExistsAdGroupItemByName;
using Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.CreateAdGroupItem
{
    public class CreateAdGroupItemCommandValidator : AbstractValidator<CreateAdGroupItemCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdGroupItemCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.AdUnitKey)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.Name)
                .MaximumLength(40)
                .CustomAsync(async (value, context, CancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupItemByNameQuery(value), CancellationToken);
                    if (result.Ok)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
