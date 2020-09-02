using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.GetAdvertiserById
{
    public class GetAdvertiserByIdQueryValidator : AbstractValidator<GetAdvertiserByIdQuery>
    {
        public GetAdvertiserByIdQueryValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");
        }
    }
}
