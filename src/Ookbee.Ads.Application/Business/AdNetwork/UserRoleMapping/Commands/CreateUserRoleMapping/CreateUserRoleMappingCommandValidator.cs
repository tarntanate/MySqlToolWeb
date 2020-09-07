﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.User.Queries.GetUserById;
using Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.GetUserRoleById;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Commands.CreateUserRoleMapping
{
    public class CreateUserRoleMappingCommandValidator : AbstractValidator<CreateUserRoleMappingCommand>
    {
        private IMediator Mediator { get; }

        public CreateUserRoleMappingCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.UserId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetUserByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.RoleId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new GetUserRoleByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });
        }
    }
}