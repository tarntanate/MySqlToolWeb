using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupByName;
using Ookbee.Ads.Application.Business.AdGroup.Queries.IsExistsAdGroupById;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandValidator : AbstractValidator<UpdateAdGroupCommand>
    {
        public IMediator Mediator { get; }

        public UpdateAdGroupCommandValidator(IMediator mediator)
        {
            Mediator = mediator;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdGroupByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var validate = context.InstanceToValidate as UpdateAdGroupCommand;
                    var result = await Mediator.Send(new GetAdGroupByNameQuery(value), cancellationToken);
                    if (result.Ok &&
                        result.Data.Id != validate.Id &&
                        result.Data.Name == value)
                        context.AddFailure($"'{context.PropertyName}' already exists.");
                });

            RuleFor(p => p.Description).MaximumLength(500);
        }
    }
}
