using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.GenerateUploadUrl
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
                .WithMessage("Only .jpg .jpeg and .png file is supported.");
        }
    }
}
