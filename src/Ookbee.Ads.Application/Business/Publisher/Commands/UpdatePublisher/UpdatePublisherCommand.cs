using System;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }
        
        public DateTime? UpdatedAt => MechineDateTime.Now.DateTime;

        public UpdatePublisherCommand()
        {
            
        }

        public UpdatePublisherCommand(string id, UpdatePublisherCommand request)
        {
            Id = id;
            Name = request.Name;
            Description = request.Description; 
            ImageUrl = request.ImageUrl;
        }
    }
}
