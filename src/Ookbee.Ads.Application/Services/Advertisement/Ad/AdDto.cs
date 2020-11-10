using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Application.Services.Advertisement.AdAsset;
using Ookbee.Ads.Application.Services.Advertisement.AdGroup;
using Ookbee.Ads.Application.Services.Advertisement.AdGroupType;
using Ookbee.Ads.Application.Services.Advertisement.AdUnit;
using Ookbee.Ads.Application.Services.Advertisement.Advertiser;
using Ookbee.Ads.Application.Services.Advertisement.Campaign;
using Ookbee.Ads.Application.Services.Advertisement.Publisher;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad
{
    public class AdDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatusType Status { get; set; }
        public int? Quota { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string LinkUrl { get; set; }
        public IEnumerable<string> Analytics { get; set; }
        public IEnumerable<AdPlatform> Platforms { get; set; }
        public IEnumerable<AdAssetDto> Assets { get; set; }
        public AdUnitDto AdUnit { get; set; }
        public CampaignDto Campaign { get; set; }

        public static AdDto FromEntity(AdEntity entity)
        {
            return entity == null
                ? null
                : Projection.Compile().Invoke(entity);
        }

        public static Expression<Func<AdEntity, AdDto>> Projection
        {
            get
            {
                return entity => new AdDto()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Description = entity.Description,
                    Status = entity.Status,
                    CountdownSecond = entity.CountdownSecond,
                    Quota = entity.Quota,
                    StartAt = entity.StartAt,
                    EndAt = entity.EndAt,
                    ForegroundColor = entity.ForegroundColor,
                    BackgroundColor = entity.BackgroundColor,
                    LinkUrl = entity.WebLink,
                    Analytics = entity.Analytics,
                    Platforms = entity.Platforms,
                    Assets = entity.AdAssets
                        .Where(asset => asset.DeletedAt == null)
                        .Select(asset => new AdAssetDto()
                        {
                            Id = asset.Id,
                            Position = asset.Position,
                            AssetType = asset.AssetType,
                            AssetPath = asset.AssetPath,
                        }),
                    AdUnit = new AdUnitDto()
                    {
                        Id = entity.AdUnit.Id,
                        AdGroup = new AdGroupDto()
                        {
                            Id = entity.AdUnit.AdGroup.Id,
                            Name = entity.AdUnit.AdGroup.Name,
                            Description = entity.AdUnit.AdGroup.Description,
                            AdGroupType = new AdGroupTypeDto()
                            {
                                Id = entity.AdUnit.AdGroup.AdGroupType.Id,
                                Name = entity.AdUnit.AdGroup.AdGroupType.Name,
                                Description = entity.AdUnit.AdGroup.AdGroupType.Description,
                            },
                            Publisher = new PublisherDto()
                            {
                                Id = entity.AdUnit.AdGroup.Publisher.Id,
                                Name = entity.AdUnit.AdGroup.Publisher.Name,
                                Description = entity.AdUnit.AdGroup.Publisher.Description,
                                ImagePath = entity.AdUnit.AdGroup.Publisher.ImagePath,
                            }
                        },
                        AdNetwork = new AdUnitNetworkDto()
                        {
                            Name = entity.AdUnit.AdNetwork,
                            AdNetworkUnits = entity.AdUnit.AdNetworks.Select(x => new AdUnitNetworkUnitIdDto()
                            {
                                Id = x.Id,
                                Platform = x.Platform,
                                AdNetworkUnitId = x.AdNetworkUnitId
                            })
                        },
                        SortSeq = entity.AdUnit.SortSeq,
                    },
                    Campaign = new CampaignDto()
                    {
                        Id = entity.Campaign.Id,
                        Name = entity.Campaign.Name,
                        Description = entity.Campaign.Description,
                        Advertiser = new AdvertiserDto()
                        {
                            Id = entity.Campaign.Advertiser.Id,
                            Name = entity.Campaign.Advertiser.Name,
                            Description = entity.Campaign.Advertiser.Description,
                            Contact = entity.Campaign.Advertiser.Contact,
                            Email = entity.Campaign.Advertiser.Email,
                            PhoneNumber = entity.Campaign.Advertiser.PhoneNumber,
                        }
                    }
                };
            }
        }
    }
}