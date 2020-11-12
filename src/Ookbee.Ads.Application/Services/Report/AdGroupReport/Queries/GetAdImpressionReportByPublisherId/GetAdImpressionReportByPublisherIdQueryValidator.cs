using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByPublisherId
{
    public class GetAdImpressionReportByCampaignIdQueryValidator : AbstractValidator<GetAdImpressionReportByPublisherIdQuery>
    {
        public GetAdImpressionReportByCampaignIdQueryValidator()
        {
            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.StartDate)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotEmpty();
        }
    }
}
