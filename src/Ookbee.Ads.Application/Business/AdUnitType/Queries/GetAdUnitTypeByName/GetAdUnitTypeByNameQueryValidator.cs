using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.AdUnitType.Queries.GetAdUnitTypeByName
{
    public class GetAdUnitTypeByNameQueryValidator : AbstractValidator<GetAdUnitTypeByNameQuery>
    {
        public GetAdUnitTypeByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
