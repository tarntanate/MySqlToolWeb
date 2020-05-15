using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetSignedUrlById
{
    public class GetSignedUrlByIdQueryValidator : AbstractValidator<GetSignedUrlByIdQuery>
    {
        public GetSignedUrlByIdQueryValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
