using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.CreateAdAsset
{
    public class CreateAdAssetCommandValidator : AbstractValidator<CreateAdAssetCommand>
    {
        private IMediator Mediator { get; }

        public CreateAdAssetCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });
        }
    }
}
