﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.User.Queries.GetUserById;
using Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleById;

namespace Ookbee.Ads.Application.Business.UserRoleMapping.Commands.UpdateUserRoleMapping
{
    public class UpdateUserRoleMappingCommandValidator : AbstractValidator<UpdateUserRoleMappingCommand>
    {
        private IMediator Mediator { get; }

        public UpdateUserRoleMappingCommandValidator(IMediator mediator)
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
