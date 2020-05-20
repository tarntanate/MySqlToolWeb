using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.PricingModel.Commands.UpdatePricingModel
{
    public class UpdatePricingModelCommandValidator : AbstractValidator<UpdatePricingModelCommand>
    {
        public UpdatePricingModelCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"PricingModel '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
