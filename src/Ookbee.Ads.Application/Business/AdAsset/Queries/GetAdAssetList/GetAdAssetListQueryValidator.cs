using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Ookbee.Ads.Application.Business.Ad.Queries.IsExistsAdById;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList
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
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("'{PropertyName}' is not a valid");
        }

        private async Task BeValidAdId(long? value, CustomContext context, CancellationToken cancellationToken)
        {
            var isExistsAdResult = await Mediator.Send(new IsExistsAdByIdQuery(value.Value));
            if (!isExistsAdResult.Ok)
                context.AddFailure(isExistsAdResult.Message);
        }
    }
}
