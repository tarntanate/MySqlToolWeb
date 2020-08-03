﻿using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Queries.GetAdGroupList
{
    public class GetAdGroupListQueryHandler : IRequestHandler<GetAdGroupListQuery, HttpResult<IEnumerable<AdGroupDto>>>
    {
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public GetAdGroupListQueryHandler(AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<IEnumerable<AdGroupDto>>> Handle(GetAdGroupListQuery request, CancellationToken cancellationToken)
        {
            return await GetListOnDb(request);
        }

        private async Task<HttpResult<IEnumerable<AdGroupDto>>> GetListOnDb(GetAdGroupListQuery request)
        {
            var result = new HttpResult<IEnumerable<AdGroupDto>>();

            var items = await AdGroupDbRepo.FindAsync(
                selector: AdGroupDto.Projection,
                filter: f => f.DeletedAt == null,
                orderBy: f => f.OrderBy(o => o.Name),
                start: request.Start,
                length: request.Length
            );

            return result.Success(items);
        }
    }
}
