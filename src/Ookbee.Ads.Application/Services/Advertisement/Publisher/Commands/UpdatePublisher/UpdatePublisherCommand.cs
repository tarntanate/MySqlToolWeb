using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : UpdatePublisherRequest, IRequest<Response<bool>>
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
