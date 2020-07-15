using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Application.Business.Analytics.Commands.CreateRequestLog;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Common;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Infrastructure;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Ookbee.Ads.Application.Business.AdUnit;

namespace Ookbee.Ads.Application.Business.Banner.Queries.GetBannerByAdUnitId
{
    public class GetBannerByAdUnitIdQueryHandler : IRequestHandler<GetBannerByAdUnitIdQuery, HttpResult<BannerResultDto>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public GetBannerByAdUnitIdQueryHandler(
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mediator = mediator;
            AdDbRepo = adDbRepo;
            AdUnitDbRepo = adUnitDbRepo;
        }
        public async Task<HttpResult<BannerResultDto>> Handle(GetBannerByAdUnitIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<BannerResultDto>> GetOnDb(GetBannerByAdUnitIdQuery request)
        {
            var result = new HttpResult<BannerResultDto>();

            var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(request.AdUnitId));
            if (!isExistsAdUnitResult.Ok)
                return result.Fail(isExistsAdUnitResult.StatusCode, isExistsAdUnitResult.Message);

            var dataAdUnitDto = await AdUnitDbRepo.FirstAsync(
                selector: AdUnitDto.Projection,
                filter: c => c.Id == request.AdUnitId);

            var banner = await AdDbRepo.FirstAsync(
                selector: BannerDto.Projection,
                filter: f =>
                    f.AdUnitId == request.AdUnitId &&
                    f.Campaign.StartDate <= MechineDateTime.Now &&
                    f.Campaign.EndDate >= MechineDateTime.Now &&
                    f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Status)
            );

            string type = dataAdUnitDto.AdNetworks.Count() > 0 ? dataAdUnitDto.AdNetworks.First().ToString() : "none";

            var createRequestLogResult = await CreateRequestLogOnDb(request, banner?.Id);
            if (!createRequestLogResult.Ok)
                return result.Fail(createRequestLogResult.StatusCode, createRequestLogResult.StatusMessage);

            if (banner.HasValue())
            {
                type = dataAdUnitDto.AdUnitType.Name;
                var baseUri = GlobalVar.AppSettings.Services.Ads.Analytics.BaseUri.External;
                var requestLogId = createRequestLogResult.Data;
                banner.AddClickUrl($"{baseUri}/api/statistics?eventId={requestLogId}&eventType=click");
                banner.AddImpressionUrl($"{baseUri}/api/statistics?eventId={requestLogId}&eventType=impression");
            }

            return result.Success(new BannerResultDto {
                Type = type,
                Banner = banner,
            });
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
