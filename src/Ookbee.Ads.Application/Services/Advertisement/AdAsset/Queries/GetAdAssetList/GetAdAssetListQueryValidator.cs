using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Services.Advertisement.Ad.Queries.IsExistsAdById;

namespace Ookbee.Ads.Application.Services.Advertisement.AdAsset.Queries.GetAdAssetList
{
    public class GetAdAssetListQueryValidator : AbstractValidator<GetAdAssetListQuery>
    {
        private IMediator Mediator { get; }

        public GetAdAssetListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);

            RuleFor(p => p.AdId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    if (value != null)
                    {
                        var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(value.Value), cancellationToken);
                        if (!isExistsAdResult.Ok)
                            context.AddFailure(isExistsAdResult.Message);
                    }
                });
        }
    }
}
