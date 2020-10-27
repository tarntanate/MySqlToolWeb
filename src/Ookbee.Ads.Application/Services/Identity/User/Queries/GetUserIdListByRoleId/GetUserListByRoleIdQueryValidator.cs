﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Identity.User.Queries.GetUserIdListByRoleId
{
    public class GetUserIdListByRoleIdQueryValidator : AbstractValidator<GetUserIdListByRoleIdQuery>
    {
        public GetUserIdListByRoleIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}