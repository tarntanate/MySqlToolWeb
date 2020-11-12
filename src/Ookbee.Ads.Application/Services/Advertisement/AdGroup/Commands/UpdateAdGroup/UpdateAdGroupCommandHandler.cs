using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandHandler : IRequestHandler<UpdateAdGroupCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdGroupEntity> AdGroupDbRepo;

        public UpdateAdGroupCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mapper = mapper;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupEntity>(request);
            await AdGroupDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
