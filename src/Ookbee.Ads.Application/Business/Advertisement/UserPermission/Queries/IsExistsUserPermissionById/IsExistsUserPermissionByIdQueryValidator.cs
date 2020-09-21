﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.UserPermission.Queries.IsExistsUserPermissionById
{
    public class IsExistsUserPermissionByIdQueryValidator : AbstractValidator<IsExistsUserPermissionByIdQuery>
    {
        public IsExistsUserPermissionByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}