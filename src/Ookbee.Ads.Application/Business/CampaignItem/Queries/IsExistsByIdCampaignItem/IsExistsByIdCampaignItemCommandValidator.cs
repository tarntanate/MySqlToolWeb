using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.IsExistsByIdCampaignItemAsset
{
    public class IsExistsByIdCampaignItemCommandValidator : AbstractValidator<IsExistsByIdCampaignItemAssetCommand>
    {
        public IsExistsByIdCampaignItemCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
