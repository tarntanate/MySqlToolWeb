using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQueryValidator : AbstractValidator<GetAdvertiserByIdQuery>
    {
        public GetAdvertiserByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .LessThanOrEqualTo(long.MaxValue)
                .WithMessage("The '{PropertyName}' is not a valid");
        }
    }
}
