﻿using MediatR;
using Ookbee.Ads.Application.Business.AdUnit.Queries.IsExistsAdUnitById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Queries.GetAdByAdUnitId
{
    public class GetAdByAdUnitIdQueryHandler : IRequestHandler<GetAdByAdUnitIdQuery, HttpResult<BannerDto>>
    {
        private IMediator Mediator { get; }
        private AdsEFCoreRepository<AdEntity> AdEFCoreRepo { get; }

        public GetAdByAdUnitIdQueryHandler(
            IMediator mediator,
            AdsEFCoreRepository<AdEntity> adEFCoreRepo)
        {
            Mediator = mediator;
            AdEFCoreRepo = adEFCoreRepo;
        }
        public async Task<HttpResult<BannerDto>> Handle(GetAdByAdUnitIdQuery request, CancellationToken cancellationToken)
        {
            return await GetOnDb(request);
        }

        private async Task<HttpResult<BannerDto>> GetOnDb(GetAdByAdUnitIdQuery request)
        {
            var result = new HttpResult<BannerDto>();

            var isExistsAdUnitResult = await Mediator.Send(new IsExistsAdUnitByIdQuery(request.AdUnitId));
            if (!isExistsAdUnitResult.Ok)
                return result.Fail(isExistsAdUnitResult.StatusCode, isExistsAdUnitResult.Message);

            var guid = Guid.NewGuid();
            var data = await AdEFCoreRepo.FirstAsync(
                selector: BannerDto.Projection,
                filter: f => f.Id == request.AdUnitId && f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => guid)
            );

            return result.Success(data);
        }
    }
}
