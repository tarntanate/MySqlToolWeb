﻿namespace Ookbee.Ads.Application.Business.AdNetwork.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupRequest
    {
        public long AdUnitTypeId { get; set; }
        public long PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}