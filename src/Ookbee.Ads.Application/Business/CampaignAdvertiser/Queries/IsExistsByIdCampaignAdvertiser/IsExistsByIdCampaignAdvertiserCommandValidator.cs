using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignAdvertiser.Queries.IsExistsByIdCampaignAdvertiser
{
    public class IsExistsByIdCampaignAdvertiserCommandValidator : AbstractValidator<IsExistsByIdCampaignAdvertiserCommand>
    {
        public IsExistsByIdCampaignAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
