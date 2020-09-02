using FluentValidation;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.User.Queries.GetUserById;
using Ookbee.Ads.Application.Business.AdNetwork.UserRole.Queries.GetUserRoleById;

namespace Ookbee.Ads.Application.Business.AdNetwork.UserRoleMapping.Queries.GetUserRoleMappingList
{
    public class GetUserRoleMappingListQueryValidator : AbstractValidator<GetUserRoleMappingListQuery>
    {
        private IMediator Mediator { get; }

        public GetUserRoleMappingListQueryValidator(IMediator mediator)
        {
            Mediator = mediator;
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(p => p.Start)
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.Length)
                .GreaterThan(0)
                .LessThanOrEqualTo(100);
        }
    }
}
