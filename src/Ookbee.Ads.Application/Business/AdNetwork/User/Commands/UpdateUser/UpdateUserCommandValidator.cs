﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.User.Queries.IsExistsUserById;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdNetwork.User.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        private IMediator Mediator { get; }

        public UpdateUserCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsUserByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.DisplayName)
                .MaximumLength(500);

            RuleFor(p => p.AvatarUrl)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");
        }
    }
}