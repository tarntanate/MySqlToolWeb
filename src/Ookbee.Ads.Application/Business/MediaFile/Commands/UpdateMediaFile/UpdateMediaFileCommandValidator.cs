using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommandValidator : AbstractValidator<UpdateMediaFileCommand>
    {
        public UpdateMediaFileCommandValidator()
        {
            RuleFor(p => p.Id).Length(24).Must(BeAValidObjectId).WithMessage(p => $"MediaFileId '{p.Id}' is not a valid 24 digit hex string.");
            RuleFor(p => p.Name).MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.MediaType).NotEmpty().MaximumLength(40);
            RuleFor(p => p.MediaUrl).MaximumLength(250);
            RuleFor(p => p.LinkUrl).MaximumLength(250);
            RuleFor(p => p.Position).NotEmpty().MaximumLength(40);
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }
    }
}
