using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Infrastructure.Enums;

namespace Ookbee.Ads.Application.Business.Banner
{
    public class BannerResultDto
    {

        public string Type { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public BannerDto Banner { get; set; }
      
    }
}