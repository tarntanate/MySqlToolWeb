using FluentValidation;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryValidator : AbstractValidator<GetPublisherByIdQuery>
    {
        public GetPublisherByIdQueryValidator()
        {
            RuleFor(p => p.Id).GreaterThan(0);
        }
    }
}
