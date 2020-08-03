using MediatR;
using Ookbee.Ads.Application.Business.Ad;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
using Ookbee.Ads.Application.Business.AdAsset;
using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList;
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
                    var getAdAssetList = await Mediator.Send(new GetAdAssetListQuery(0, 100, adId.Value));
                    if (getAdAssetList.Ok)
                    {
                        adAssets = getAdAssetList.Data.ToList();
                    }
                }
            }

            var createRequestLogResult = await CreateRequestLogOnDb(request, adId);
            if (!createRequestLogResult.Ok)
                return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.StatusMessage);

            var banner = new BannerDto();
            banner.Ad = !ad.HasValue() ? null : new BannerAdDto()
            {
                CountdownSecond = ad.CountdownSecond,
                ForegroundColor = ad.ForegroundColor,
                BackgroundColor = ad.BackgroundColor,
                LinkUrl = ad.LinkUrl,
                Assets = adAssets.Where(asset => asset.DeletedAt == null).Select(asset => new BannerAssetDto()
                {
                    AssetPath = asset.AssetPath,
                    AssetType = asset.AssetType,
                    Position = asset.Position,
                })
            };
            banner.Analytics = new BannerAnalyticsDto()
            {
                Clicks = new List<string>(),
                Impressions = !ad.HasValue() ? new List<string>() : ad.Analytics.ToList<string>(),
            };
            banner.AdUnitType = getAdUnitById.Data.AdUnitType.Name;

            if (banner?.Analytics != null)
            {
                var baseUri = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var eventId = createRequestLogResult.Data;
                banner.Analytics.Clicks.Insert(0, $"{baseUri}/events/{eventId}/click");
                banner.Analytics.Impressions.Insert(0, $"{baseUri}/events/{eventId}/impression");
            }

            return result.Success(banner);
        }

        private async Task<long?> GetAvalibleAdId(long adUnitId)
        {
            var ads = await Mediator.Send(new GetAdListQuery(0, 100, adUnitId, null));
            var adIds = ads.Data.Select(f => f.Id).ToList();
            var adId = adIds.OrderBy(x => new Random().Next()).Take(1).FirstOrDefault();
            var testValue = new long[] { 0, adId };
            var index = new Random().Next(testValue.Length);
            return index > 0 ? adId : (long?)null;
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
