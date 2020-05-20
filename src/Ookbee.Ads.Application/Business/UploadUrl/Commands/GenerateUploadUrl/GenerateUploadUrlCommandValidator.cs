using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandValidator : AbstractValidator<GenerateUploadUrlCommand>
    {
        public GenerateUploadUrlCommandValidator()
        {
            RuleFor(p => p.MapperId).Must(BeAValidObjectId).WithMessage(p => $"MapperId '{p.MapperId}' is not a valid 24 digit hex string.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
