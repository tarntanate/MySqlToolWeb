using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByAdId
{
    public class GetAdClickReportByAdIdQueryValidator : AbstractValidator<GetAdClickReportByAdIdQuery>
    {
        public GetAdClickReportByAdIdQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
