using System;
using System.Linq.Expressions;
using Newtonsoft.Json;
using Ookbee.Ads.Application.Business.AdUnit;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Application.Business.Publisher;
using Ookbee.Ads.Application.Business.AdUnitType;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Ad
{
    public class AdDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string AppLink { get; set; }
        public string WebLink { get; set; }
        public List<string> Analytics { get; set; }
        public List<string> Platforms { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public AdUnitDto AdUnit { get; set; }

        public static Expression<Func<AdEntity, AdDto>> Projection
        {
            get
            {
                return entity => new AdDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    CountdownSecond = entity.CountdownSecond,
                    ForegroundColor = entity.ForegroundColor,
                    BackgroundColor = entity.BackgroundColor,
                    AppLink = entity.AppLink,
                    WebLink = entity.WebLink,
                    Analytics = entity.Analytics,
                    Platforms = entity.Platforms,
                    AdUnit = new AdUnitDto()
                    {
                        Id = entity.AdUnit.Id,
                        Name = entity.AdUnit.Name,
                        Description = entity.AdUnit.Description,
                        AdUnitType = new AdUnitTypeDto()
                        {
                            Id = entity.AdUnit.AdUnitType.Id,
                            Name = entity.AdUnit.AdUnitType.Name,
                            Description = entity.AdUnit.AdUnitType.Description,
                        },
                        Publisher = new PublisherDto()
                        {
                            Id = entity.AdUnit.Publisher.Id,
                            Name = entity.AdUnit.Publisher.Name,
                            Description = entity.AdUnit.Publisher.Description,
                        }
                    }
                };
            }
        }
    }
}