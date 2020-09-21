using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommandHandler : IRequestHandler<UpdateAdNetworkCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo { get; }

        public UpdateAdNetworkCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdNetworkCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdNetworkEntity>(request);
            await AdNetworkDbRepo.UpdateAsync(entity.Id, entity);
            await AdNetworkDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<bool>();
            return result.Success(true);
        }
    }
}
