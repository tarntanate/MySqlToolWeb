using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.CreateAdGroupType
{
    public class CreateAdGroupTypeCommandHandler : IRequestHandler<CreateAdGroupTypeCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public CreateAdGroupTypeCommandHandler(
            IMapper mapper,
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            Mapper = mapper;
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<long>> Handle(CreateAdGroupTypeCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<AdGroupTypeEntity>(request);
            await AdGroupTypeDbRepo.InsertAsync(entity);
            await AdGroupTypeDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().OK(entity.Id);
        }
    }
}
