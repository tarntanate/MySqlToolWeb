﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryValidator : AbstractValidator<GetPublisherByNameQuery>
    {
        public GetPublisherByNameQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.CountryCode)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10);
        }
    }
}
