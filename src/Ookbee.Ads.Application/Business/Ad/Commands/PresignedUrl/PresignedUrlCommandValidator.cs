using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Commands.PresignedUrl
{
    public class PresignedUrlCommandValidator : AbstractValidator<PresignedUrlCommand>
    {
        public PresignedUrlCommandValidator()
        {
            RuleFor(p => p.Id).Must(BeAValidObjectId).WithMessage(p => $"Ad '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.FileExtension).Must(BeAValidJpeg).WithMessage("Only .jpg and .jpeg file is supported.");
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
