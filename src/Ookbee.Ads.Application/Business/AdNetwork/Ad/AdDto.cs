using Ookbee.Ads.Application.Business.AdNetwork.AdAsset;
using Ookbee.Ads.Application.Business.AdNetwork.AdGroup;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnit;
using Ookbee.Ads.Application.Business.AdNetwork.AdUnitType;
using Ookbee.Ads.Application.Business.AdNetwork.Advertiser;
using Ookbee.Ads.Application.Business.AdNetwork.Campaign;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.AdNetwork.Ad
{
    public class AdDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public int? Quota { get; set; }
        public DateTimeOffset? StartAt { get; set; }
        public DateTimeOffset? EndAt { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string LinkUrl { get; set; }
        public IEnumerable<string> Analytics { get; set; }
        public IEnumerable<Platform> Platforms { get; set; }
        public IEnumerable<AdAssetDto> Assets { get; set; }
        public AdUnitDto AdUnit { get; set; }
        public CampaignDto Campaign { get; set; }

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
                            Position = asset.Position,
                            AssetType = asset.AssetType,
                            AssetPath = asset.AssetPath,
                        }),
                    AdUnit = new AdUnitDto()
                    {
                        Id = entity.AdUnit.Id,
                        AdNetwork = entity.AdUnit.AdNetwork,
                        AdNetworkUnitId = entity.AdUnit.AdNetworkUnitId,
                        SortSeq = entity.AdUnit.SortSeq,
                        AdGroup = new AdGroupDto()
                        {
                            Id = entity.AdUnit.AdGroup.Id,
                            Name = entity.AdUnit.AdGroup.Name,
                            Description = entity.AdUnit.AdGroup.Description,
                            AdUnitType = new AdUnitTypeDto()
                            {
                                Id = entity.AdUnit.AdGroup.AdUnitType.Id,
                                Name = entity.AdUnit.AdGroup.AdUnitType.Name,
                                Description = entity.AdUnit.AdGroup.AdUnitType.Description,
                            }
                        }
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