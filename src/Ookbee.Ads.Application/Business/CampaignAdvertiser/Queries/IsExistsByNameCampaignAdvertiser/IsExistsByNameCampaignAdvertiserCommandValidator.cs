using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByNameCampaignAdvertiser
{
    public class IsExistsByNameCampaignAdvertiserCommandValidator : AbstractValidator<IsExistsByNameCampaignAdvertiserCommand>
    {
        public IsExistsByNameCampaignAdvertiserCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
