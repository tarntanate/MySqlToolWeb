using MediatR;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetByAdId;
using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById;
using Ookbee.Ads.Application.Business.Analytics.Commands.CreateRequestLog;
using Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByAdUnitId;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetAdUnitById
{
    public class GetBannerByAdUnitIdHandler : IRequestHandler<GetBannerByAdUnitIdQuery, HttpResult<BannerDto>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetBannerByAdUnitIdHandler(IMediator mediator, AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<BannerDto>> Handle(GetBannerByAdUnitIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<BannerDto>> GetOnDb(GetBannerByAdUnitIdQuery request)
        {
            var result = new HttpResult<BannerDto>();

            AdDto ad = null;
            List<AdAssetDto> adAssets = null;

            var getAdUnitById = await Mediator.Send(new GetAdUnitByIdQuery(request.AdUnitId));
            if (!getAdUnitById.Ok)
                return result.Fail(getAdUnitById.StatusCode, getAdUnitById.Message);

            var adId = await GetAvalibleAdId(request.AdUnitId);
            if (adId.HasValue())
            {
                var getAdById = await Mediator.Send(new GetAdByIdQuery(adId.Value));
                if (getAdById.Ok)
                {
                    ad = getAdById.Data;
                    var getAdAssetByAdId = await Mediator.Send(new GetAdAssetByAdIdQuery(adId.Value));
                    if (getAdAssetByAdId.Ok)
                    {
                        adAssets = getAdAssetByAdId.Data.ToList();
                    }
                }
            }

            var createRequestLogResult = await CreateRequestLogOnDb(request, adId);
            if (!createRequestLogResult.Ok)
                return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.StatusMessage);

            var banner = new BannerDto()
            {
                Ad = !ad.HasValue() ? null : new BannerAdDto()
                {
                    Id = ad.Id,
                    Name = ad.Name,
                    CountdownSecond = ad.CountdownSecond,
                    ForegroundColor = ad.ForegroundColor,
                    BackgroundColor = ad.BackgroundColor,
                    LinkUrl = ad.LinkUrl,
                    Analytics = new BannerAnalyticsDto()
                    {
                        Clicks = new List<string>(),
                        Impressions = ad.Analytics.ToList<string>(),
                    },
                    Assets = adAssets.Select(asset => new BannerAssetDto()
                    {
                        Id = asset.Id,
                        AssetPath = asset.AssetPath,
                        AssetType = asset.AssetType,
                        Position = asset.Position,
                    })
                },
                AdUnitType = getAdUnitById.Data.Name,
                AdNetworks = getAdUnitById.Data.AdNetworks,
            };

            if (banner?.Ad?.Analytics != null)
            {
                var baseUri = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var eventId = createRequestLogResult.Data;
                banner.Ad.Analytics.Clicks.Insert(0, $"{baseUri}/api/events/{eventId}/click");
                banner.Ad.Analytics.Impressions.Insert(0, $"{baseUri}/api/events/{eventId}/impression");
            }

            return result.Success(banner);
        }

        private async Task<long?> GetAvalibleAdId(long adUnitId)
        {
            var ads = await Mediator.Send(new GetAdListQuery(0, 100, adUnitId, null));
            var adIds = ads.Data.Select(f => f.Id).ToList();
            var adId = adIds.OrderBy(x => new Random().Next()).Take(1).FirstOrDefault();
            return adId > 0 ? adId : default;
        }

        private async Task<HttpResult<long>> CreateRequestLogOnDb(GetBannerByAdUnitIdQuery request, long? adId = null)
        {
            var result = new HttpResult<long>();

            var createRequestLogCommand = new CreateRequestLogCommand()
            {
                AdId = adId,
                AdUnitId = request.AdUnitId,
                Platform = request.Platform,
                AppCode = request.AppCode,
                AppVersion = request.AppVersion,
                DeviceId = request.DeviceId,
                UserAgent = request.UserAgent,
                IsFill = adId != null ? true : false
            };
            var createRequestLogResult = await Mediator.Send(createRequestLogCommand);
            if (!createRequestLogResult.Ok)
                return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.StatusMessage);

            return result.Success(createRequestLogResult.Data);
        }
    }
}
