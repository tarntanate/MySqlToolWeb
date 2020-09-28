using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdNetwork.Commands.UpdateAdNetwork
{
    public class UpdateAdNetworkCommandHandler : IRequestHandler<UpdateAdNetworkCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdNetworkEntity> AdNetworkDbRepo;

        public UpdateAdNetworkCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdNetworkEntity> adNetworkDbRepo)
        {
            Mapper = mapper;
            AdNetworkDbRepo = adNetworkDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdNetworkCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdNetworkEntity>(request);
            await AdNetworkDbRepo.UpdateAsync(entity.Id, entity);
            await AdNetworkDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
