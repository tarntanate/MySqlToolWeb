using FluentValidation;

namespace Ookbee.Ads.Application.Services.Analytics.AdStats.Queries.GetAvailableQuota
{
    public class GetAvailableQueryValidator : AbstractValidator<GetAvailableQuotaQuery>
    {
        public GetAvailableQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
