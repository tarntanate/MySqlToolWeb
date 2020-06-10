using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandValidator : AbstractValidator<UpdateAdAssetCommand>
    {
        public UpdateAdAssetCommandValidator()
        {
            RuleFor(p => p.AdId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.AssetPath).NotNull().NotEmpty().MaximumLength(40);
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
