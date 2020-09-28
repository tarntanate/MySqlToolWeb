using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAdQuotaById
{
    public class GetAdQuotaByIdQueryValidator : AbstractValidator<GetAdQuotaByIdQuery>
    {
        public GetAdQuotaByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
