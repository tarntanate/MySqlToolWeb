using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.DeleteAdGroupItem
{
    public class DeleteAdGroupItemCommandValidator : AbstractValidator<DeleteAdGroupItemCommand>
    {
        public DeleteAdGroupItemCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
