using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup.Queries.IsExistsAdGroupByName;
using Ookbee.Ads.Application.Services.Advertisement.AdUnitType.Queries.IsExistsAdUnitTypeById;
using Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommandValidator : AbstractValidator<CreateAdGroupCommand>
    {
        public IMediator Mediator { get; }

        public CreateAdGroupCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdUnitTypeId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdUnitTypeByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.PublisherId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsPublisherByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByNameQuery(value), cancellationToken);
                    if (result.Ok)
                        context.AddFailure("'{PropertyName}' already exists.");
                });

            RuleFor(p => p.Description)
                .MaximumLength(500);
        }
    }
}
