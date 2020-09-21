﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.DeleteAdNetwork
{
    public class DeleteAdNetworkCommandValidator : AbstractValidator<DeleteAdNetworkCommand>
    {
        public DeleteAdNetworkCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
