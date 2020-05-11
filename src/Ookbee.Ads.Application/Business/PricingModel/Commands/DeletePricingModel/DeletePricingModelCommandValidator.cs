using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.DeletePricingModel
{
    public class DeletePricingModelCommandValidator : AbstractValidator<DeletePricingModelCommand>
    {
        public DeletePricingModelCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
