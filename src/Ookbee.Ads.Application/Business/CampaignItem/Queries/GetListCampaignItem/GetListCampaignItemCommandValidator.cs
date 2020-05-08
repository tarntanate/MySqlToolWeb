using FluentValidation;

namespace Ookbee.Ads.Application.Business.CampaignItem.Queries.GetListCampaignItem
{
    public class GetListCampaignItemCommandValidator : AbstractValidator<GetListCampaignItemCommand>
    {
        public GetListCampaignItemCommandValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
