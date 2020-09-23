using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetByAdId
{
    public class GetAdAssetByAdIdQueryValidator : AbstractValidator<GetAdAssetByAdIdQuery>
    {
        public GetAdAssetByAdIdQueryValidator()
        {
            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
