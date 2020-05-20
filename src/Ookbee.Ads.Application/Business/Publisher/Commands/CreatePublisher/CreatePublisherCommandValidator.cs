using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
    {
        public CreatePublisherCommandValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.ImageUrl).MaximumLength(250);
        }
    }
}
