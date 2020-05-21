using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Commands.CreateAdUploadUrl
{
    public class CreateAdUploadUrlCommandValidator : AbstractValidator<CreateAdUploadUrlCommand>
    {
        public CreateAdUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.FileExtension).NotNull().NotEmpty().Must(BeAValidJpeg).WithMessage("Only .jpg and .jpeg file is supported.");
        }

        private bool BeAValidObjectId(string value)
        {
            return ObjectId.TryParse(value, out ObjectId objectId);
        }

        private bool BeAValidJpeg(string value)
        {
            return value.IsValidJpeg();
        }
    }
}
