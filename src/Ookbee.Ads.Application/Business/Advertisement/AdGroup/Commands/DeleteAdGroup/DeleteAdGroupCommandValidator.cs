using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.AdGroup.Commands.DeleteAdGroup
{
    public class DeleteAdGroupCommandValidator : AbstractValidator<DeleteAdGroupCommand>
    {
        public DeleteAdGroupCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
