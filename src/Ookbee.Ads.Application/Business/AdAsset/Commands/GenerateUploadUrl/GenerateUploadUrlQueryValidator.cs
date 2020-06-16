﻿using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.AdAsset.Commands.GenerateUploadUrl
{
    public class GenerateUploadUrlCommandValidator : AbstractValidator<GenerateUploadUrlCommand>
    {
        public GenerateUploadUrlCommandValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0).LessThanOrEqualTo(long.MaxValue);
            RuleFor(p => p.Extension).NotNull().NotEmpty().Must(BeAValidFileExtension).WithMessage("Only .jpg .jpeg and .png file is supported.");
        }

        private bool BeAValidFileExtension(string value)
        {
            return value.IsValidJpeg() || value.IsValidPng();
        }
    }
}