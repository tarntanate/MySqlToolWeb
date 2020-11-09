using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherByName;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
    {
        private readonly IMediator Mediator;

        public CreatePublisherCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var command = context.InstanceToValidate as CreatePublisherCommand;
                    var result = await Mediator.Send(new GetPublisherByNameQuery(command.Name, command.CountryCode), cancellationToken);
                    if (result.IsSuccess)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.ImagePath)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.CountryCode)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(10);
        }
    }
}
