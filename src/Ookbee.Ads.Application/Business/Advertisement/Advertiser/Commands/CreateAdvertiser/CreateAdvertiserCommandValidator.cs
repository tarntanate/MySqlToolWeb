using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Advertisement.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandValidator : AbstractValidator<CreateAdvertiserCommand>
    {
        public IMediator Mediator { get; set; }

        public CreateAdvertiserCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdvertiserByNameQuery(value), cancellationToken);
                    if (result.Ok)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.Contact)
                .MaximumLength(5000);

            RuleFor(p => p.ImagePath)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidUri())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.Email)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidEmailAddress())
                .WithMessage("'{PropertyName}' address is not valid");

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(10)
                .Must(value => !value.HasValue() || value.IsValidPhoneNumber())
                .WithMessage("'{PropertyName}' is not valid");
        }
    }
}
