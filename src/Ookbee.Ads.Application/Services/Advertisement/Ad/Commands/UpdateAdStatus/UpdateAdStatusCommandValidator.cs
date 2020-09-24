using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommandValidator : AbstractValidator<UpdateAdStatusCommand>
    {
        private readonly IMediator Mediator;

        public UpdateAdStatusCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
                    if (!isExistsAdResult.IsSuccess)
                        context.AddFailure(isExistsAdResult.Message);
                });

            RuleFor(p => p.Status)
                .Custom((value, context) =>
                {
                    if (value == AdStatusType.Unknown)
                        context.AddFailure($"Unsupported Status Type.");
                });
        }
    }
}
