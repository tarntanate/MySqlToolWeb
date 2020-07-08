using FluentValidation;

namespace Ookbee.Ads.Application.Business.ActivityLog.Queries.GetActivityLogList
{
    public class GetActivityLogListQueryValidator : AbstractValidator<GetActivityLogListQuery>
    {
        public GetActivityLogListQueryValidator()
        {
            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
