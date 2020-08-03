using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupByName;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommandValidator : AbstractValidator<CreateAdGroupCommand>
    {
        public IMediator Mediator { get; }

        public CreateAdGroupCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByNameQuery(value), cancellationToken);
                    if (result.Ok)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
