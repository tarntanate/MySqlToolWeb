using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Commands.DeletePublisher
{
    public class DeletePublisherCommand : IRequest<Response<bool>>
    {
        public long Id { get; set; }

        public DeletePublisherCommand(long id)
        {
            Id = id;
        }
    }
}
