using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.User.Queries.IsExistsUserById
{
    public class IsExistsUserByIdQuery : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public IsExistsUserByIdQuery(long id)
        {
            Id = id;
        }
    }
}
