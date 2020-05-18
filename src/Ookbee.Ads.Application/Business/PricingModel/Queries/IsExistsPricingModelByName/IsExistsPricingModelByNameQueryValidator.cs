using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelByName
{
    public class IsExistsPricingModelByNameQueryValidator : AbstractValidator<IsExistsPricingModelByNameQuery>
    {
        public IsExistsPricingModelByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
