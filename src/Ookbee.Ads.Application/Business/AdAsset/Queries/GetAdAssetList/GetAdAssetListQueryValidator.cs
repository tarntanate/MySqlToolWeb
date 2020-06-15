using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList
{
    public class GetAdAssetListQueryValidator : AbstractValidator<GetAdAssetListQuery>
    {
        public GetAdAssetListQueryValidator()
        {
            RuleFor(p => p.AdId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue).When(m => m.AdId != null);
            RuleFor(p => p.Start).GreaterThanOrEqualTo(0);
            RuleFor(p => p.Length).GreaterThan(0).LessThanOrEqualTo(100);
        }
    }
}
