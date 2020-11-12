using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.UpdateAd
{
    public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public UpdateAdCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.UpdateAsync(entity.Id, entity);
            await AdDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
