using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelByName
{
    public class GetPricingModelByNameQueryValidator : AbstractValidator<GetPricingModelByNameQuery>
    {
        public GetPricingModelByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
        }
    }
}
