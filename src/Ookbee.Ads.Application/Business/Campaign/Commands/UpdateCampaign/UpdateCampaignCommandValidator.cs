using FluentValidation;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common;

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
            RuleFor(p => p.StartDate).GreaterThanOrEqualTo(MechineDateTime.Now);
            RuleFor(p => p.EndDate).GreaterThanOrEqualTo(MechineDateTime.Now);
        }
    }
}
