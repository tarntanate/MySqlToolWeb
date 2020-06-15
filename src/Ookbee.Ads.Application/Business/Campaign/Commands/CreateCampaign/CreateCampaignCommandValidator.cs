using FluentValidation;
using Ookbee.Ads.Infrastructure.Enums;
using Ookbee.Ads.Common;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(p => p.AdvertiserId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.StartDate).GreaterThanOrEqualTo(MechineDateTime.Now);
            RuleFor(p => p.EndDate).GreaterThanOrEqualTo(MechineDateTime.Now);
        }
    }
}
