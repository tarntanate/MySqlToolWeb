using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.DeleteAdvertiser
{
    public class DeleteAdvertiserCommandValidator : AbstractValidator<DeleteAdvertiserCommand>
    {
        public DeleteAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Advertiser '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
