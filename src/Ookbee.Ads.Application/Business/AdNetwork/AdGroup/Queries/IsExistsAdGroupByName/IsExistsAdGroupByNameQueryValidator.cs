﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Queries.IsExistsAdGroupByName
{
    public class IsExistsAdGroupByNameQueryValidator : AbstractValidator<IsExistsAdGroupByNameQuery>
    {
        public IsExistsAdGroupByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}