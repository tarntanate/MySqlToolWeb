using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandValidator : AbstractValidator<DeleteAdUnitTypeCommand>
    {
        public DeleteAdUnitTypeCommandValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
