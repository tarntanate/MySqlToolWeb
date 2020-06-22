using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
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
                .MaximumLength(40);

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

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdvertiserByNameQuery(value));
            if (result.Ok)
                context.AddFailure($"Advertiser '{value}' already exists.");
        }
    }
}
