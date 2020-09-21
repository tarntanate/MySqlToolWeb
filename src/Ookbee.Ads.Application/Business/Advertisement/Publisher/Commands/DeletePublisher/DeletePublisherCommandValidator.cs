using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
    {
        public DeletePublisherCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
