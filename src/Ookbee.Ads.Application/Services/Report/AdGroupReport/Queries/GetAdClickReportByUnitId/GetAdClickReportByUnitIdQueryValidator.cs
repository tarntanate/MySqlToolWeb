using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdClickReportByUnitId
{
    public class GetAdClickReportByAdIdQueryValidator : AbstractValidator<GetAdClickReportByUnitIdQuery>
    {
        public GetAdClickReportByAdIdQueryValidator()
        {
            RuleFor(p => p.UnitId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
