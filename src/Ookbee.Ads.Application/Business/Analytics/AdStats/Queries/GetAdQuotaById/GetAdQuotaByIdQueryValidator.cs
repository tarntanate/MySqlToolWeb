using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdStats.Queries.GetAdQuotaById
{
    public class GetAdQuotaByIdQueryValidator : AbstractValidator<GetAdQuotaByIdQuery>
    {
        public GetAdQuotaByIdQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
