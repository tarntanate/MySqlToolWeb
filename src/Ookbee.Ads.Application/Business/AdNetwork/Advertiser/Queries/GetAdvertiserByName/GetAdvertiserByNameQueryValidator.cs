using FluentValidation;

namespace Ookbee.Ads.Application.Business.AdNetwork.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQueryValidator : AbstractValidator<GetAdvertiserByNameQuery>
    {
        public GetAdvertiserByNameQueryValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(40);
        }
    }
}
