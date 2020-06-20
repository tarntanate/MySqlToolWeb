using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandValidator : AbstractValidator<UpdateAdvertiserCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdvertiserCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");

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
                .Must(value => !value.HasValue() && value.IsValidUri())
                .WithMessage("The '{PropertyName}' address is not valid");

            RuleFor(p => p.Email)
                .MaximumLength(255)
                .Must(value => !value.HasValue() || value.IsValidEmailAddress())
                .WithMessage("Please specify a valid 'Email'.");

            RuleFor(p => p.PhoneNumber)
                .MaximumLength(10)
                .Must(value => !value.HasValue() || value.IsValidPhoneNumber())
                .WithMessage("Please specify a valid 'PhoneNumber'.").MaximumLength(10);

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
