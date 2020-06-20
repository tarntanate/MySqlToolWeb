using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName;
using Ookbee.Ads.Common.Extensions;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
    {
        private IMediator Mediator { get; }

        public UpdatePublisherCommandValidator(IMediator mediator)
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

            RuleFor(p => p.ImagePath)
                .MaximumLength(255)
                .Must(value => value.HasValue() && value.IsValidUri())
                .WithMessage("The '{PropertyName}' address is not valid");

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var validate = context.InstanceToValidate as UpdatePublisherCommand;
            if (validate.Id == 0)
                context.AddFailure($"Publisher '{validate.Id}' doesn't exist.");

            var result = await Mediator.Send(new GetPublisherByNameQuery(value));
            if (result.Ok &&
                result.Data.Id != validate.Id &&
                result.Data.Name == value)
                context.AddFailure($"Publisher '{value}' already exists.");
        }
    }
}
