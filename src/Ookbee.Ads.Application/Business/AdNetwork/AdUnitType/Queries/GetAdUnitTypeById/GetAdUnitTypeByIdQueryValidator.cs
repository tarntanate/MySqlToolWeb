﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQueryValidator : AbstractValidator<GetAdUnitTypeByIdQuery>
    {
        public GetAdUnitTypeByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}