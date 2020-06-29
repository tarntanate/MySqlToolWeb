using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.DeleteAdUnit
{
    public class DeleteAdUnitCommandValidator : AbstractValidator<DeleteAdUnitCommand>
    {
        public DeleteAdUnitCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
