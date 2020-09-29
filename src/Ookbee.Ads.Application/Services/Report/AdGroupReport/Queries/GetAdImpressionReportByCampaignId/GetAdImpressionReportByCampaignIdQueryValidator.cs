using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByCampaignId
{
    public class GetAdImpressionReportByCampaignIdQueryValidator : AbstractValidator<GetAdImpressionReportByCampaignIdQuery>
    {
        public GetAdImpressionReportByCampaignIdQueryValidator()
        {
            RuleFor(p => p.CampaignId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
