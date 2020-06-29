using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQueryValidator : AbstractValidator<IsExistsAdAssetByIdQuery>
    {
        public IsExistsAdAssetByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
