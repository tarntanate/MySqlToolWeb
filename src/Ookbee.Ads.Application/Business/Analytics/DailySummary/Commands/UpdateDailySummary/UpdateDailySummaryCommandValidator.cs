using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class UpdateDailySummaryCommandValidator : AbstractValidator<UpdateDailySummaryCommand>
    {
        public UpdateDailySummaryCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
        }
    }
}
