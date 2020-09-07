using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.IsExistsAStatsByKey
{
    public class IsExistsAdAssetStatsByKeyQueryValidator : AbstractValidator<IsExistsAdAssetStatsByKeyQuery>
    {
        public IsExistsAdAssetStatsByKeyQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
