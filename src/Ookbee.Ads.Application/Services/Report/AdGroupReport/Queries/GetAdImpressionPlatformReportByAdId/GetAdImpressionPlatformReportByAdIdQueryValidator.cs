using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionPlatformReportByAdId
{
    public class GetAdImpressionPlatformReportByAdIdQueryValidator : AbstractValidator<GetAdImpressionPlatformReportByAdIdQuery>
    {
        public GetAdImpressionPlatformReportByAdIdQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
            
            RuleFor(p => p.StartDate)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotEmpty();
        }
    }
}
