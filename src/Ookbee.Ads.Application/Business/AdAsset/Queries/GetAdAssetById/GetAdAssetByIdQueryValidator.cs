using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetById
{
    public class GetAdAssetByIdQueryValidator : AbstractValidator<GetAdAssetByIdQuery>
    {
        public GetAdAssetByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
