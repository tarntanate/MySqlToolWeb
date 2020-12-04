﻿using MediatR;
using Ookbee.Ads.Common;
using Ookbee.Ads.Common.Response;
using System;

namespace Ookbee.Ads.Application.Business.RequestLogs.AdClickLog.Commands.CreateAdClickLog
{
    public class CreateAdClickLogCommand : IRequest<Response<bool>>
    {
        public DateTime CreatedAt { get; set; }
        public short PlatformId { get; set; }
        public int AdId { get; set; }
        public int UnitId { get; set; }
        public int CampaignId { get; set; }
        public string UUID { get; set; }

        public CreateAdClickLogCommand(int adId, int campaignId, int unitId, short platformId, string uuid)
        {
            CreatedAt = MechineDateTime.Now.DateTime;
            AdId = adId;
            UnitId = unitId;
            CampaignId = campaignId;
            PlatformId = platformId;
            UUID = uuid;
        }
    }
}