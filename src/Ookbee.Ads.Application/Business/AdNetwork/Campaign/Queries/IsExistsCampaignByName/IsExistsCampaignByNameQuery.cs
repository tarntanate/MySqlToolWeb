﻿using MediatR;
using Ookbee.Ads.Common.Result;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Queries.IsExistsCampaignByName
{
    public class IsExistsCampaignByNameQuery : IRequest<HttpResult<bool>>
    {
        public string Name { get; set; }

        public IsExistsCampaignByNameQuery(string name)
        {
            Name = name;
        }
    }
}