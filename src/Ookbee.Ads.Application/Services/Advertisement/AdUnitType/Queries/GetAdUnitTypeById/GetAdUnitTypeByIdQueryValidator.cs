﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.GetAdUnitTypeById
{
    public class GetAdUnitTypeByIdQueryValidator : AbstractValidator<GetAdUnitTypeByIdQuery>
    {
        public GetAdUnitTypeByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
