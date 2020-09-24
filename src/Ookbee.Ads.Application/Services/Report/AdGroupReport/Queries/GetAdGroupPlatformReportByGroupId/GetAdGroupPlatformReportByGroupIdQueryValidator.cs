using FluentValidation;

namespace Ookbee.Ads.Application.Business.Report.AdGroupReport.Queries.GetAdGroupPlatformReportByGroupId
{
    public class GetAdGroupPlatformReportByGroupIdQueryValidator : AbstractValidator<GetAdGroupPlatformReportByGroupIdQuery>
    {
        public GetAdGroupPlatformReportByGroupIdQueryValidator()
        {
            RuleFor(p => p.AdGroupId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
