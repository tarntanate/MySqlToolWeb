using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommand : CreatePublisherRequest, IRequest<HttpResult<long>>
    {
        public CreatePublisherCommand(CreatePublisherRequest request)
        {
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
        }
    }
}
