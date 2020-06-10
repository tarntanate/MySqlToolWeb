using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : IRequest<HttpResult<bool>>
    {
        public long Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImagePath { get; set; }

        public UpdatePublisherCommand()
        {
            
        }

        public UpdatePublisherCommand(long id, UpdatePublisherCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description;
            ImagePath = request.ImagePath;
        }
    }
}
