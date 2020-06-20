using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeByName;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.UpdateAdUnitType
{
    public class UpdateAdUnitTypeCommandValidator : AbstractValidator<UpdateAdUnitTypeCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdUnitTypeCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Name).CustomAsync(BeAValidName);
            RuleFor(p => p.Description).MaximumLength(500);
        }

        private async Task BeAValidName(string value, CustomContext context, CancellationToken cancellationToken)
        {
            var validate = context.InstanceToValidate as UpdateAdUnitTypeCommand;
            if (validate.Id == 0)
                context.AddFailure($"AdUnitType '{validate.Id}' doesn't exist.");

            var result = await Mediator.Send(new GetAdUnitTypeByNameQuery(value));
            if (result.Ok &&
                result.Data.Id != validate.Id &&
                result.Data.Name == value)
                context.AddFailure($"AdUnitType '{value}' already exists.");
        }
    }
}
