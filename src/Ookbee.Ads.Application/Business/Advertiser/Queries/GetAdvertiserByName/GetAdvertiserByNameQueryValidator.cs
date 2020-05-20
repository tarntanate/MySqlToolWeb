using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName
{
    public class GetAdvertiserByNameQueryValidator : AbstractValidator<GetAdvertiserByNameQuery>
    {
        public GetAdvertiserByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
