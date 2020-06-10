using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommandValidator : AbstractValidator<CreateAdAssetCommand>
    {
        public CreateAdAssetCommandValidator()
        {
            RuleFor(p => p.AdId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.AssetPath).Must(BeAValidUri).WithMessage((rule, value) => $"Invalid AssetPath '{value}'");

        }

        private bool BeAValidUri(string value)
        {
            if (value.HasValue())
                return value.IsValidUri();
            return true;
        }
    }
}