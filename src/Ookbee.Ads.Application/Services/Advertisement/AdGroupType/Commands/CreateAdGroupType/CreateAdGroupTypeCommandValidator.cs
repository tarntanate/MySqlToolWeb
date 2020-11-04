using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Queries.IsExistsAdGroupTypeByName;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.CreateAdGroupType
{
    public class CreateAdGroupTypeCommandValidator : AbstractValidator<CreateAdGroupTypeCommand>
    {
        public IMediator Mediator { get; }

        public CreateAdGroupTypeCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupTypeByNameQuery(value), cancellationToken);
                    if (result.IsSuccess)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
