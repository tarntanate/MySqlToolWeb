using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdImpressionReportByUnitId
{
    public class GetAdImpressionReportByUnitIdQueryValidator : AbstractValidator<GetAdImpressionReportByUnitIdQuery>
    {
        public GetAdImpressionReportByUnitIdQueryValidator()
        {
            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
            
            RuleFor(p => p.StartDate)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotEmpty();
        }
    }
}
