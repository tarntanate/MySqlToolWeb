﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommandValidator : AbstractValidator<DeleteUserPermissionCommand>
    {
        public DeleteUserPermissionCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}