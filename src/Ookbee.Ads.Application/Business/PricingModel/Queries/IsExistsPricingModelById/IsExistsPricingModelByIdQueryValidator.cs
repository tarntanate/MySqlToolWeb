using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.IsExistsPricingModelById
{
    public class IsExistsPricingModelByIdQueryValidator : AbstractValidator<IsExistsPricingModelByIdQuery>
    {
        public IsExistsPricingModelByIdQueryValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"PricingModel '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
