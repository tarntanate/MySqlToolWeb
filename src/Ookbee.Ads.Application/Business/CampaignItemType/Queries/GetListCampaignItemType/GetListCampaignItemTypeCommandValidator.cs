using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItemType.Queries.GetListCampaignItemType
{
    public class GetListCampaignItemTypeCommandValidator : AbstractValidator<GetListCampaignItemTypeCommand>
    {
        public GetListCampaignItemTypeCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
