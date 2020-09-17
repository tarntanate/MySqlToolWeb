﻿using Ookbee.Ads.Infrastructure.Models;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.CreateAdNetwork
{
    public class CreateAdNetworkRequest
    {
        public long AdUnitId { get; set; }
        public string AdNetworkUnitId { get; set; }
        public Platform Platform { get; set; }
    }
}
