using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQueryValidator : AbstractValidator<GetAdAssetByIdQuery>
    {
        public GetAdAssetByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
