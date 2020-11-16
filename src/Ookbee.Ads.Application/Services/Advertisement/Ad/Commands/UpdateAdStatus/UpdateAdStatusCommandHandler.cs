using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommandHandler : IRequestHandler<UpdateAdStatusCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public UpdateAdStatusCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.UpdateAsync(entity.Id, entity, x => x.Status);
            await AdDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
