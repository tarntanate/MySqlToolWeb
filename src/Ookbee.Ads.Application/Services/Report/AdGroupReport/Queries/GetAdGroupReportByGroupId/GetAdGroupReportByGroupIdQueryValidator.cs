using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupReportByGroupId
{
    public class GetAdGroupReportByGroupIdQueryValidator : AbstractValidator<GetAdGroupReportByGroupIdQuery>
    {
        public GetAdGroupReportByGroupIdQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
            
            RuleFor(p => p.StartDate)
                .NotEmpty();
            RuleFor(p => p.EndDate)
                .NotEmpty();
        }
    }
}
