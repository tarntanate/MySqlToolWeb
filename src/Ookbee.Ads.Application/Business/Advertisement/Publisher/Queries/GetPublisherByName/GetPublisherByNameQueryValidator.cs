﻿using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryValidator : AbstractValidator<GetPublisherByNameQuery>
    {
        public GetPublisherByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}