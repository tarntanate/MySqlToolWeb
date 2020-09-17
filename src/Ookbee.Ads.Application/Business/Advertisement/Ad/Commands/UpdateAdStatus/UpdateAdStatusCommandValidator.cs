using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Business.Advertisement.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommandValidator : AbstractValidator<UpdateAdStatusCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdStatusCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
                    if (!isExistsAdResult.Ok)
                        context.AddFailure(isExistsAdResult.Message);
                });

            RuleFor(p => p.Status)
                .NotNull();
        }
    }
}
