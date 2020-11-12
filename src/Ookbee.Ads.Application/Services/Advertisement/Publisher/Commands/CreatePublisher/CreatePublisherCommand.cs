using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherCommand : IRequest<Response<long>>
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ImagePath { get; private set; }
        public string CountryCode { get; private set; }

        public CreatePublisherCommand(CreatePublisherRequest request)
        {
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
            CountryCode = request.CountryCode;
        }
    }
}
