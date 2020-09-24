using FluentValidation;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetByPosition
{
    public class GetAdAssetByPositionValidator : AbstractValidator<GetAdAssetByPositionQuery>
    {
        public GetAdAssetByPositionValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.Position)
                .Custom((value, context) =>
                {
                    if (value == AdPosition.Unknown)
                        context.AddFailure($"Unsupported Position Type.");
                });
        }
    }
}
