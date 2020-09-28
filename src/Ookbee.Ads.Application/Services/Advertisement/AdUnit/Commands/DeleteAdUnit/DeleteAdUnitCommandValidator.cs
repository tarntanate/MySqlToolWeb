using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommandValidator : AbstractValidator<DeleteAdUnitCommand>
    {
        public DeleteAdUnitCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
