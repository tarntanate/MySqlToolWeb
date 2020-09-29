using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByAdId
{
    public class GetAdImpressionReportByAdIdQueryValidator : AbstractValidator<GetAdImpressionReportByAdIdQuery>
    {
        public GetAdImpressionReportByAdIdQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
