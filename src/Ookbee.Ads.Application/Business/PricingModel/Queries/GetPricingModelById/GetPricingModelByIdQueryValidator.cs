using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelById
{
    public class GetPricingModelByIdQueryValidator : AbstractValidator<GetPricingModelByIdQuery>
    {
        public GetPricingModelByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"PricingModel '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
