using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommandValidator : AbstractValidator<CreateAdAssetCommand>
    {
        private readonly IMediator Mediator;

        public CreateAdAssetCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
                    if (!result.IsSuccess)
                        context.AddFailure(result.Message);
                });
        }
    }
}
