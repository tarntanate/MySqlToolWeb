using Ookbee.Ads.Application.Business.AdAsset;
using Ookbee.Ads.Application.Business.AdGroup;
using Ookbee.Ads.Application.Business.AdUnit;
using Ookbee.Ads.Application.Business.AdUnitType;
using Ookbee.Ads.Application.Business.Advertiser;
using Ookbee.Ads.Application.Business.Campaign;
using Ookbee.Ads.Application.Infrastructure;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Ookbee.Ads.Application.Business.Ad
{
    public class AdDto : DefaultDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public AdStatus Status { get; set; }
        public int? CountdownSecond { get; set; }
        public string ForegroundColor { get; set; }
        public string BackgroundColor { get; set; }
        public string LinkUrl { get; set; }
        public List<string> Analytics { get; set; }
        public List<Platform> Platforms { get; set; }
        public List<AdAssetDto> Assets { get; set; }
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
                    }).ToList(),
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
                        StartDate = entity.Campaign.StartDate,
                        EndDate = entity.Campaign.EndDate,
                        PricingModel = entity.Campaign.PricingModel,
                        Budget = entity.Campaign.CampaignCost.Budget,
                        CostPerUnit = entity.Campaign.CampaignCost.CostPerUnit,
                        Quota = entity.Campaign.CampaignImpression.Quota,
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