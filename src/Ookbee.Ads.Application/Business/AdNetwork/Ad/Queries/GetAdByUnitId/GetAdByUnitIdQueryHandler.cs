// using MediatR;
// using Ookbee.Ads.Application.Business.Ad.Queries.GetAdById;
// using Ookbee.Ads.Application.Business.Ad.Queries.GetAdList;
// using Ookbee.Ads.Application.Business.AdAsset.Queries.GetAdAssetList;
// using Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByKeyQuery;
// using Ookbee.Ads.Application.Business.AdUnit.Queries.GetAdUnitById;
// using Ookbee.Ads.Common.Extensions;
// using Ookbee.Ads.Common.Result;
// using Ookbee.Ads.Domain.Entities.AdsEntities;
// using Ookbee.Ads.Infrastructure;
// using Ookbee.Ads.Persistence.EFCore.AdsDb;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;

// namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByUnitId
// {
//     public class GetAdByUnitIdQueryHandler : IRequestHandler<GetAdByUnitIdQuery, HttpResult<AdDto>>
//     {
//         private IMediator Mediator { get; }
//         private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

//         public GetAdByUnitIdQueryHandler(IMediator mediator, AdsDbRepository<AdUnitEntity> adUnitDbRepo)
//         {
//             Mediator = mediator;
//             AdUnitDbRepo = adUnitDbRepo;
//         }

//         public async Task<HttpResult<AdDto>> Handle(GetAdByUnitIdQuery request, CancellationToken cancellationToken)
//         {
//             return await GetOnDb(request);
//         }

//         private async Task<HttpResult<AdDto>> GetOnDb(GetAdByUnitIdQuery request)
//         {
//             var result = new HttpResult<AdDto>();

//             var ad = new AdDto();
//             var adAssets = new List<AdAssetDto>();

//             var getAdUnitById = await Mediator.Send(new GetAdUnitByIdQuery(request.AdUnitId));
//             if (!getAdUnitById.Ok)
//                 return result.Fail(getAdUnitById.StatusCode, getAdUnitById.Message);

//             var adId = await GetAvalibleAdId(request.AdUnitId);
//             if (adId.HasValue())
//             {
//                 var getAdById = await Mediator.Send(new GetAdByIdQuery(adId.Value));
//                 if (getAdById.Ok)
//                 {
//                     ad = getAdById.Data;
//                     var getAdAssetList = await Mediator.Send(new GetAdAssetListQuery(0, 100, adId.Value));
//                     if (getAdAssetList.Ok)
//                     {
//                         adAssets = getAdAssetList.Data.ToList();
//                     }
//                 }
//             }

//             var createRequestLogResult = await CreateRequestLogOnDb(request, adId);
//             if (!createRequestLogResult.Ok)
//                 return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.StatusMessage);

//             var adData = new AdDataDto();
//             adData.Ad = !ad.HasValue() ? null : new BannerAdDto()
//             {
//                 CountdownSecond = ad.CountdownSecond,
//                 ForegroundColor = ad.ForegroundColor,
//                 BackgroundColor = ad.BackgroundColor,
//                 LinkUrl = ad.LinkUrl,
//                 Assets = adAssets.Where(asset => asset.DeletedAt == null).Select(asset => new BannerAssetDto()
//                 {
//                     AssetPath = asset.AssetPath,
//                     AssetType = asset.AssetType,
//                     Position = asset.Position,
//                 })
//             };
//             adData.Analytics = new BannerAnalyticsDto()
//             {
//                 Clicks = new List<string>(),
//                 Impressions = !ad.HasValue() ? new List<string>() : ad.Analytics.ToList<string>(),
//             };
//             adData.AdUnitType = getAdUnitById.Data.AdUnitType.Name;

//             if (adData?.Analytics != null)
//             {
//                 var baseUri = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
//                 var eventId = createRequestLogResult.Data;
//                 adData.Analytics.Clicks.Insert(0, $"{baseUri}/events/{eventId}/click");
//                 adData.Analytics.Impressions.Insert(0, $"{baseUri}/events/{eventId}/impression");
//             }

//             return result.Success(adData);
//         }


















//         private async Task<long?> GetAvalibleAdId(long adUnitId)
//         {
//             var ads = await Mediator.Send(new GetAdListQuery(0, 100, adUnitId, null));
//             var adIds = ads.Data.Select(f => f.Id).ToList();
//             var adId = adIds.OrderBy(x => new Random().Next()).Take(1).FirstOrDefault();
//             var testValue = new long[] { 0, adId };
//             var index = new Random().Next(testValue.Length);
//             return index > 0 ? adId : (long?)null;
//         }
//     }
// }
