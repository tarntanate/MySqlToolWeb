using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.DeleteAdUnitType
{
    public class DeleteAdUnitTypeCommandValidator : AbstractValidator<DeleteAdUnitTypeCommand>
    {
        public DeleteAdUnitTypeCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
        }
    }
}
