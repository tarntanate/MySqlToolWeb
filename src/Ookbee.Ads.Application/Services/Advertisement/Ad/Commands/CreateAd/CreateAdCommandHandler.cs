using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Ad.Commands.CreateAd
{
    public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdEntity> AdDbRepo;

        public CreateAdCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            AdDbRepo = adDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.InsertAsync(entity);
            await AdDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().OK(entity.Id);
        }
    }
}
