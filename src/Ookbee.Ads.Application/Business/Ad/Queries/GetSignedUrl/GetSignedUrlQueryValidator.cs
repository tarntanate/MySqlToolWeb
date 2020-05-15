using FluentValidation;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl
{
    public class GetSignedUrlQueryValidator : AbstractValidator<GetSignedUrlQuery>
    {
        public GetSignedUrlQueryValidator()
        {
            RuleFor(p => p.FileExtension).Must(BeAValidJpg).WithMessage("Only .jpg and .jpeg file is supported.");
        }

        private bool BeAValidJpg(string fileExtension)
        {
            return fileExtension.IsValidJpeg();
        }
    }
}
