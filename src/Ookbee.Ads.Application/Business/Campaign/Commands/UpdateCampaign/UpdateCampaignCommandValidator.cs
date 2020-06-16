using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandValidator : AbstractValidator<UpdateCampaignCommand>
    {
        public UpdateCampaignCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.AdvertiserId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            // RuleFor(p => p.StartDate).Must(BeAValidISO9601).WithMessage("Only ISO8601 string formats.");
            // RuleFor(p => p.EndDate).Must(BeAValidISO9601).WithMessage("Only ISO8601 string formats.");
        }

        private bool BeAValidISO9601(string value)
        {
            return value.IsValidISO8601();
        }
    }
}
