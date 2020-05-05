using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.IsExistsByIdCampaignItemType
{
    public class IsExistsByIdCampaignItemTypeCommandValidator : AbstractValidator<IsExistsByIdCampaignItemTypeCommand>
    {
        public IsExistsByIdCampaignItemTypeCommandValidator()
        {
            RuleFor(p => p.Id).NotEmpty();
        }
    }
}
