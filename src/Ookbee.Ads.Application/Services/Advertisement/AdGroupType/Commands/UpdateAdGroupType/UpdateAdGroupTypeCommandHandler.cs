using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.UpdateAdGroupType
{
    public class UpdateAdGroupTypeCommandHandler : IRequestHandler<UpdateAdGroupTypeCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public UpdateAdGroupTypeCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            Mapper = mapper;
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateAdGroupTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupTypeEntity>(request);
            await AdGroupTypeDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupTypeDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
