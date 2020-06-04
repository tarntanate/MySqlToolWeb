using System;
using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.MediaFile.Commands.UpdateUploadUrl
{
    public class UpdateUploadUrlCommand : IRequest<HttpResult<bool>>
    {
        public string Id { get; set; }

        public string UploadUrlId { get; set; }

        public UpdateUploadUrlCommand()
        {
            
        }

        public UpdateUploadUrlCommand(string id, UpdateUploadUrlCommand request)
        {
            Id = id;
            UploadUrlId = request.UploadUrlId;
        }
    }
}
