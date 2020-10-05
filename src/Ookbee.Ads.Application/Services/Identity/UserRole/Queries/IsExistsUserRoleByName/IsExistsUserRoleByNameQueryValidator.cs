﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.UserRole.Queries.IsExistsUserRoleByName
{
    public class IsExistsUserRoleByNameQueryValidator : AbstractValidator<IsExistsUserRoleByNameQuery>
    {
        public IsExistsUserRoleByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}