using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.IsExistsByIdCampaignItemAsset
{
    public class IsExistsByIdCampaignItemAssetCommandValidator : AbstractValidator<IsExistsByIdCampaignItemAssetCommand>
    {
        public IsExistsByIdCampaignItemAssetCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
