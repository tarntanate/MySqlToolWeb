using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Advertiser.Queries.GetByIdAdvertiser
{
    public class GetByIdAdvertiserCommandValidator : AbstractValidator<GetByIdAdvertiserCommand>
    {
        public GetByIdAdvertiserCommandValidator()
        {
            RuleFor(p => p.Id).Length(24);
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"'{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
