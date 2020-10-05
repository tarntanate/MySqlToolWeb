﻿using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommand : IRequest<Response<long>>
    {
        public long AdUnitTypeId { get; private set; }
        public long PublisherId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public CreateAdGroupCommand(CreateAdGroupRequest request)
        {
            AdUnitTypeId = request.AdUnitTypeId;
            PublisherId = request.PublisherId;
            Name = request.Name;
            Description = request.Description;
        }
    }
}