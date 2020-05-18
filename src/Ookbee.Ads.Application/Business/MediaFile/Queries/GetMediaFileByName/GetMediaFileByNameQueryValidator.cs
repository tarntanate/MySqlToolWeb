using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.MediaFile.Queries.GetMediaFileByName
{
    public class GetMediaFileByNameQueryValidator : AbstractValidator<GetMediaFileByNameQuery>
    {
        public GetMediaFileByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
