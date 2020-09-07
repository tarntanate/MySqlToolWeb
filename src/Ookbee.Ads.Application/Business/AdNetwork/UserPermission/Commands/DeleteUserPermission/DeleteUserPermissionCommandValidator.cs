﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserPermission.Commands.DeleteUserPermission
{
    public class DeleteUserPermissionCommandValidator : AbstractValidator<DeleteUserPermissionCommand>
    {
        public DeleteUserPermissionCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}