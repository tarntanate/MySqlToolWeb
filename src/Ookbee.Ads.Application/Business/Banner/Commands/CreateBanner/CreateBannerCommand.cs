﻿using MediatR;
using MongoDB.Bson;
using Ookbee.Ads.Common.Result;
using System;

namespace Ookbee.Ads.Application.Business.Banner.Commands.CreateBanner
{
    public class CreateBannerCommand : IRequest<HttpResult<string>>
    {
        public string Id => ObjectId.GenerateNewId().ToString();

        public string CampaignId { get; set; }

        public string SlotTypeId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public TimeSpan Cooldown { get; set; }

        public string ForegroundColor { get; set; }

        public string BackgroundColor { get; set; }

        public CreateBannerCommand()
        {
            
        }

        public CreateBannerCommand(CreateBannerCommand request)
        {
            CampaignId =request.CampaignId;
            SlotTypeId = request.SlotTypeId;
            Name = request.Name;
            Description = request.Description;
            Cooldown = request.Cooldown;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
        }
    }
}
