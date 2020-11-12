﻿using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQueryHandler : IRequestHandler<IsExistsPublisherByNameQuery, Response<bool>>
    {
        private readonly AdsDbRepository<PublisherEntity> PublisherDbRepo;

        public IsExistsPublisherByNameQueryHandler(
            AdsDbRepository<PublisherEntity> publisherDbRepo)
        {
            PublisherDbRepo = publisherDbRepo;
        }

        public async Task<Response<bool>> Handle(IsExistsPublisherByNameQuery request, CancellationToken cancellationToken)
        {
            var isExists = await PublisherDbRepo.AnyAsync(f =>
                f.Name.ToUpper() == request.Name.ToUpper() &&
                f.CountryCode.ToUpper() == request.CountryCode.ToUpper() &&
                f.DeletedAt == null
            );

            var result = new Response<bool>();
            return (isExists)
                ? result.OK(true)
                : result.NotFound();
        }
    }
}
