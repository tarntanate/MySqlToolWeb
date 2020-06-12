using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.Queries.GetRequestLogById
{
    public class GetRequestLogByIdQueryValidator : AbstractValidator<GetRequestLogByIdQuery>
    {
        public GetRequestLogByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
