using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.CommitUploadUrl
{
    public class CommitUploadUrlCommandValidator : AbstractValidator<CommitUploadUrlCommand>
    {
        public CommitUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"UploadUrl '{p.Id}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
