﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.UserPermission.Queries.GetUserPermissionByName
{
    public class GetUserPermissionByNameQueryValidator : AbstractValidator<GetUserPermissionByNameQuery>
    {
        public GetUserPermissionByNameQueryValidator()
        {
            RuleFor(p => p.ExtensionName)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
