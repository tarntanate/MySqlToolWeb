using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByCampaignId
{
    public class GetAdImpressionReportByCampaignIdQueryValidator : AbstractValidator<GetAdImpressionPlatformReportByCampaignIdQuery>
    {
        public GetAdImpressionReportByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
