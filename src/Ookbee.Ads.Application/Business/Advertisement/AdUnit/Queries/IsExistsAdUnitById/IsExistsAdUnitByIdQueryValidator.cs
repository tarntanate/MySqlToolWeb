﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnit.Queries.IsExistsAdUnitById
{
    public class IsExistsAdUnitByIdQueryValidator : AbstractValidator<IsExistsAdUnitByIdQuery>
    {
        public IsExistsAdUnitByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}