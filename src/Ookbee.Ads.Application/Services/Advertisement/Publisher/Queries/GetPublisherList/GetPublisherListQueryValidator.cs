﻿using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQueryValidator : AbstractValidator<GetPublisherListQuery>
    {
        public GetPublisherListQueryValidator()
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