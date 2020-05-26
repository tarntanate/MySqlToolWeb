using FluentValidation;

namespace Ookbee.RequestLogs.Application.Business.RequestLog.Queries.GetRequestLogList
{
    public class GetRequestLogListQueryValidator : AbstractValidator<GetRequestLogListQuery>
    {
        public GetRequestLogListQueryValidator()
        {
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
