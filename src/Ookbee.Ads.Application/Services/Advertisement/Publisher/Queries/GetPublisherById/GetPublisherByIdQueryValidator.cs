using FluentValidation;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherById
{
    public class GetPublisherByIdQueryValidator : AbstractValidator<GetPublisherByIdQuery>
    {
        public GetPublisherByIdQueryValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
