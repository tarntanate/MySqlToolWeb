﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandValidator : AbstractValidator<DeleteAdUnitTypeCommand>
    {
        public DeleteAdUnitTypeCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}