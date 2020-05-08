using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItemAsset.Queries.GetListCampaignItemAsset
{
    public class GetListCampaignItemAssetCommandValidator : AbstractValidator<GetListCampaignItemAssetCommand>
    {
        public GetListCampaignItemAssetCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
