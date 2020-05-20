﻿using MediatR;
using Ookbee.Ads.Common.Result;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.PricingModel.Queries.GetPricingModelList
{
    public class GetPricingModelListQuery : IRequest<HttpResult<IEnumerable<PricingModelDto>>>
    {
        public int Start { get; set; }
        
        public int Length { get; set; }

        public GetPricingModelListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
