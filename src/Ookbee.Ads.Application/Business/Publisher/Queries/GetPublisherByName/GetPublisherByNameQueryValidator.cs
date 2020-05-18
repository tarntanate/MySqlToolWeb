using FluentValidation;
using MongoDB.Bson;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.GetPublisherByName
{
    public class GetPublisherByNameQueryValidator : AbstractValidator<GetPublisherByNameQuery>
    {
        public GetPublisherByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
