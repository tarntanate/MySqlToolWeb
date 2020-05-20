﻿using FluentValidation;
using MongoDB.Bson;
using Ookbee.Ads.Common.Extensions;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetSignedUrl
{
    public class GetSignedUrlQueryValidator : AbstractValidator<GetSignedUrlQuery>
    {
        public GetSignedUrlQueryValidator()
        {
            RuleFor(p => p.MapperId).Must(BeAValidObjectId).WithMessage(p => $"Mapper '{p.MapperId}' is not a valid 24 digit hex string.");
            RuleFor(p => p.FileExtension).Must(BeAValidJpeg).WithMessage("Only .jpg and .jpeg file is supported.");
        }

        private bool BeAValidObjectId(string id)
        {
            return ObjectId.TryParse(id, out ObjectId objectId);
        }

        private bool BeAValidJpeg(string fileExtension)
        {
            return fileExtension.IsValidJpeg();
        }
    }
}
