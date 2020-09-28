using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.IsExistsAdAssetById
{
    public class IsExistsAdAssetByIdQueryValidator : AbstractValidator<IsExistsAdAssetByIdQuery>
    {
        public IsExistsAdAssetByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
