using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : IRequest<Response<bool>>
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }

        public UpdatePublisherCommand(long id, UpdatePublisherRequest request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
        }
    }
}
