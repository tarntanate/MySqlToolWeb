using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.IsExistsAdUnitTypeByName;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommandValidator : AbstractValidator<CreateAdUnitTypeCommand>
    {
        public IMediator Mediator { get; }

        public CreateAdUnitTypeCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);

            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.Name)
                .CustomAsync(BeAValidName);
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new IsExistsAdUnitTypeByNameQuery(value));
            if (result.Ok)
                context.AddFailure($"AdUnitType '{value}' already exists.");
        }
    }
}
