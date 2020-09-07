using FluentValidation;

namespace Ookbee.Ads.Application.Business.Analytics.AdAssetStats.Queries.IsExistsAdAssetStatsById
{
    public class IsExistsAdAssetStatsByIdQueryValidator : AbstractValidator<IsExistsAdAssetStatsByIdQuery>
    {
        public IsExistsAdAssetStatsByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
