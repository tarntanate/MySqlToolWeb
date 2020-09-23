using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommand : CreatePublisherRequest, IRequest<Response<long>>
    {
        public CreatePublisherCommand(CreatePublisherRequest request)
        {
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
        }
    }
}
