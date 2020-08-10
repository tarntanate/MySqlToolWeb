using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByUnitId
{
    public class GetAdByUnitIdQueryValidator : AbstractValidator<GetAdByUnitIdQuery>
    {
        private IMediator Mediator { get; }

        public GetAdByUnitIdQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.AdUnitId)
                .GreaterThan(0)
                .CustomAsync(async (value, context, cancellationToken) =>
                {
                    var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(value), cancellationToken);
                    if (!isExistsAdUnitResult.Ok)
                        context.AddFailure(isExistsAdUnitResult.Message);
                });
        }
    }
}
