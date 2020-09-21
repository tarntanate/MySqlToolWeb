using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommandValidator : AbstractValidator<DeleteAdAssetCommand>
    {
        public DeleteAdAssetCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
