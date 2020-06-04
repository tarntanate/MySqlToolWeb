using System;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateMediaFile
{
    public class UpdateMediaFileCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string AdId { get; set; }

        public string Position { get; set; }
        
        public DateTime? UpdatedAt => MechineDateTime.Now.DateTime;

        public UpdateMediaFileCommand()
        {

        }

        public UpdateMediaFileCommand(string id, UpdateMediaFileCommand request)
        {
            Id = id;
            AdId = request.AdId;
            Position = request.Position;
        }
    }
}
