using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.DeleteAdAsset
{
    public class DeleteAdAssetCommandValidator : AbstractValidator<DeleteAdAssetCommand>
    {
        public DeleteAdAssetCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
