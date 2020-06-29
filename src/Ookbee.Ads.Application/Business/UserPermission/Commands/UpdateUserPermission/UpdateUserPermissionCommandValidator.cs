﻿using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.UserPermission.Queries.GetUserPermissionByName;
using Ookbee.Ads.Application.Business.UserPermission.Queries.IsExistsUserPermissionById;
using Ookbee.Ads.Application.Business.UserRole.Queries.GetUserRoleById;

namespace Ookbee.Ads.Application.Business.UserPermission.Commands.UpdateUserPermission
{
    public class UpdateUserPermissionCommandValidator : AbstractValidator<UpdateUserPermissionCommand>
    {
        private IMediator Mediator { get; }

        public UpdateUserPermissionCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsUserPermissionByIdQuery(value), cancellationToken);
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

            RuleFor(p => p.ExtensionName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateUserPermissionCommand;
                    var result = await Mediator.Send(new GetUserPermissionByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.ExtensionName == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });
        }
    }
}
