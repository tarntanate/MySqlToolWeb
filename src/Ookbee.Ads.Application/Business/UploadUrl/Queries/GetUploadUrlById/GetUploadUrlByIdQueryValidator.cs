using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.UploadUrl.Queries.GetUploadUrlById
{
    public class GetUploadUrlByIdQueryValidator : AbstractValidator<GetUploadUrlByIdQuery>
    {
        public GetUploadUrlByIdQueryValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"Id '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
