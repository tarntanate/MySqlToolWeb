﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.DeleteAdNetwork
{
    public class DeleteAdNetworkCommandValidator : AbstractValidator<DeleteAdNetworkCommand>
    {
        public DeleteAdNetworkCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;
            
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
