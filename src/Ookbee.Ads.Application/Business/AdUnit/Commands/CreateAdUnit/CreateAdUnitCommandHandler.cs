using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheAdByUnitId;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateCacheUnitListByGroupId;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnit.Commands.CreateAdUnit
{
    public class CreateAdUnitCommandHandler : IRequestHandler<CreateAdUnitCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitEntity> AdUnitDbRepo { get; }

        public CreateAdUnitCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitEntity> adUnitDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitDbRepo = adUnitDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdUnitCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdUnitCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<AdUnitEntity>(request);
            await AdUnitDbRepo.InsertAsync(entity);
            await AdUnitDbRepo.SaveChangesAsync();

            await Mediator.Send(new CreateCacheAdByUnitIdCommand(entity.Id));

            return result.Success(entity.Id, entity.Id, entity);
        }
    }
}
