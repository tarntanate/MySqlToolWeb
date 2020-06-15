using FluentValidation;
using Ookbee.Ads.Common.Extensions;
using System.Linq;

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
            if (!value.HasValue())
                return false;
            var platforms = new string[] { "click", "display", "impression" };
            return platforms.Contains(value.ToLower());
        }
    }
}
