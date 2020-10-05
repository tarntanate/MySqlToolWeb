﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeByName;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommandValidator : AbstractValidator<CreateAdUnitTypeCommand>
    {
        public IMediator Mediator { get; }

        public CreateAdUnitTypeCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitTypeByNameQuery(value), cancellationToken);
                    if (result.IsSuccess)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}