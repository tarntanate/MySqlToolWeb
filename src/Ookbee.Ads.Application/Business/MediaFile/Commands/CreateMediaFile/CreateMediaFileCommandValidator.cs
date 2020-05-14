using System;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Formatters;
using Ookbee.Ads.Application.Infrastructure.Enums;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.CreateMediaFile
{
    public class CreateMediaFileCommandValidator : AbstractValidator<CreateMediaFileCommand>
    {
        public CreateMediaFileCommandValidator()
        {
            RuleFor(p => p.Name).MaximumLength(40);
            RuleFor(p => p.Description).MaximumLength(500);
            RuleFor(p => p.MediaType).NotEmpty().MaximumLength(40);
            RuleFor(p => p.MediaUrl).MaximumLength(250);
            RuleFor(p => p.LinkUrl).MaximumLength(250);
            RuleFor(p => p.Position).Must(BeAValidPosition).WithMessage(p => $"The Position '{p.Position}' is not supported.");
        }

        private bool BeAValidMediaType(string value)
        {
            return Enum.TryParse(value, true, out MediaType mediaType);
        }

        private bool BeAValidMediaUrl(string value)
        {
            return value.IsValidUri();
        }

        private bool BeAValidPosition(string value)
        {
            return Enum.TryParse(value, true, out Position position);
        }
    }
}