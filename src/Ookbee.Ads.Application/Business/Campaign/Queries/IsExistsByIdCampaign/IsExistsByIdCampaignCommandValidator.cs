using FluentValidation;

namespace Ookbee.Ads.Application.Business.Campaign.Queries.IsExistsByIdCampaign
{
    public class IsExistsByIdCampaignCommandValidator : AbstractValidator<IsExistsByIdCampaignCommand>
    {
        public IsExistsByIdCampaignCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
