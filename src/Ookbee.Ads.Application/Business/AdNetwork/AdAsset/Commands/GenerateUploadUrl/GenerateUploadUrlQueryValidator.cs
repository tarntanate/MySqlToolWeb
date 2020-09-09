using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdNetwork.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandValidator : AbstractValidator<GenerateUploadUrlCommand>
    {
        public GenerateUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0)
                .WithMessage("'{PropertyName}' is not a valid");

            RuleFor(p => p.Extension)
                .Must(value => value.HasValue() && (value.IsValidJpeg() || value.IsValidPng()))
                .WithMessage("The image field only accept files with the following extensions: .jpg .jpeg and .png");
        }
    }
}
