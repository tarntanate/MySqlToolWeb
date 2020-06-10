using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public DeletePublisherCommand(long id)
        {
            Id = id;
        }
    }
}
