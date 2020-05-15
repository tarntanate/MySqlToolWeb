using FluentValidation;

namespace Ookbee.Ads.Application.Business.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryValidator : AbstractValidator<IsExistsPublisherByNameQuery>
    {
        public IsExistsPublisherByNameQueryValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
        }
    }
}
