﻿using FluentValidation;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common.Extensions;
using System;

namespace Ookbee.Ads.Application.Business.Analytics.Commands.UpdateRequestLogEvent
{
    public class UpdateRequestLogEventCommandValidator : AbstractValidator<UpdateRequestLogEventCommand>
    {
        public UpdateRequestLogEventCommandValidator()
        {
            RuleFor(p => p.EventId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.EventType).Must(BeAValidEventType).WithMessage($"'Event Type' only support 'Click', 'Display' and 'Impression'.");
        }

        private bool BeAValidEventType(string value)
        {
            if (value.HasValue() && Enum.TryParse<AdsEvent>(value, true, out var adsEvent))
                return true;
            return true;
        }
    }
}