using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.GetAdAssetStatsByKey
{
    public class GetAdAssetStatsByKeyQueryValidator : AbstractValidator<GetAdStatsByKeyQuery>
    {
        public GetAdAssetStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
