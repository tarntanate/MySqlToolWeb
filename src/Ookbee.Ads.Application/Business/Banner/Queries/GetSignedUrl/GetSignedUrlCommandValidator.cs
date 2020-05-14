using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetSignedUrl
{
    public class GetSignedUrlCommandValidator : AbstractValidator<GetSignedUrlCommand>
    {
        public GetSignedUrlCommandValidator()
        {
            RuleFor(p => p.FileExtension).Must(BeAValidJpg).WithMessage("Only .jpg and .jpeg file is supported.");
        }

        private bool BeAValidJpg(string fileExtension)
        {
            return fileExtension.IsValidJpeg();
        }
    }
}
