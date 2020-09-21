using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.Advertisement.Ad.Queries.IsExistsAdById;
using Ookbee.Ads.Application.Business.Advertisement.AdAsset.Queries.IsExistsAdAssetById;

namespace Ookbee.Ads.Application.Business.Advertisement.AdAsset.Commands.UpdateAdAsset
{
    public class UpdateAdAssetCommandValidator : AbstractValidator<UpdateAdAssetCommand>
    {
        private IMediator Mediator { get; }

        public UpdateAdAssetCommandValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var result = await Mediator.Send(new IsExistsAdAssetByIdQuery(value), cancellationToken);
                    if (!result.Ok)
                        context.AddFailure(result.Message);
                });

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
