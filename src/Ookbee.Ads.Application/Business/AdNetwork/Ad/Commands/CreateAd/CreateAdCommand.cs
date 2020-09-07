﻿using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Commands.CreateAd
{
    public class CreateAdCommand : CreateAdRequest, IRequest<HttpResult<long>>
    {
        public CreateAdCommand(CreateAdRequest request)
        {
            AdUnitId = request.AdUnitId;
            CampaignId = request.CampaignId;
            Name = request.Name;
            Description = request.Description;
            Status = request.Status;
            Quota = request.Quota;
            StartAt = request.StartAt;
            EndAt = request.EndAt;
            ForegroundColor = request.ForegroundColor;
            BackgroundColor = request.BackgroundColor;
            Analytics = request.Analytics;
            Platforms = request.Platforms;
            AppLink = request.AppLink;
            LinkUrl = request.LinkUrl;
        }
    }
}