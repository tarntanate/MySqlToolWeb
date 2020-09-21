using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : UpdatePublisherRequest, IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }

        public UpdatePublisherCommand(long id, UpdatePublisherRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
        }
    }
}
