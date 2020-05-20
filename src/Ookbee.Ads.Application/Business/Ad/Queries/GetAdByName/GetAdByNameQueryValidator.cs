using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByName
{
    public class GetAdByNameQueryValidator : AbstractValidator<GetAdByNameQuery>
    {
        public GetAdByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
