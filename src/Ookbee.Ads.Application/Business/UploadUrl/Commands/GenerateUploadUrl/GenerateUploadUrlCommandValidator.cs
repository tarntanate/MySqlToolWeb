using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.UploadUrl.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandValidator : AbstractValidator<GenerateUploadUrlCommand>
    {
        public GenerateUploadUrlCommandValidator()
        {
            RuleFor(p => p.MapperId).Must(BeAValidObjectId).WithMessage(p => $"MapperId '{p.MapperId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.MapperType).NotNull().NotEmpty();
            RuleFor(p => p.Bucket).NotNull().NotEmpty();
            RuleFor(p => p.Key).NotNull().NotEmpty();
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }
    }
}
