using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandValidator : AbstractValidator<CreateAdUnitCommand>
    {
        public CreateAdUnitCommandValidator()
        {
            RuleFor(p => p.AdUnitTypeId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.PublisherId).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
