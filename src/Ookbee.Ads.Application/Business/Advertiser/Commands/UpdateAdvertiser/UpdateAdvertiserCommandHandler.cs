using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserByName;
using Ookbee.Ads.Application.Business.Advertiser.Queries.GetAdvertiserById;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System;
using System.Threading;
using System.Threading.Tasks;
using Ookbee.Ads.Domain.Entities;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.UpdateAdvertiser
{
    public class UpdateAdvertiserCommandHandler : IRequestHandler<UpdateAdvertiserCommand, HttpResult<bool>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public UpdateAdvertiserCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdvertiserCommand request)
        {
            var result = new HttpResult<bool>();

            var advertiserResult = await Mediator.Send(new GetAdvertiserByIdQuery(request.Id));
            if (!advertiserResult.Ok)
                return result.Fail(advertiserResult.StatusCode, advertiserResult.Message);

            var advertiserByNameResult = await Mediator.Send(new GetAdvertiserByNameQuery(request.Name));
            if (advertiserByNameResult.Ok &&
                advertiserByNameResult.Data.Id != request.Id &&
                advertiserByNameResult.Data.Name == request.Name)
                return result.Fail(409, $"Advertiser '{request.Name}' already exists.");

            var source = Mapper
                .Map(request)
                .Over(advertiserResult.Data);
            var entity = Mapper
                .Map(source)
                .ToANew<AdvertiserEntity>();

            await AdvertiserDbRepo.UpdateAsync(entity.Id, entity);
            await AdvertiserDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
