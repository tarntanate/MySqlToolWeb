using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.Commands.CreateRequestLog
{
    public class CreateRequestLogCommandValidator : AbstractValidator<CreateRequestLogCommand>
    {
        public CreateRequestLogCommandValidator()
        {
            RuleFor(p => p.AdUnitId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
