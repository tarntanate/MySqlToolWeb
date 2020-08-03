using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.UpdateAdGroup
{
    public class UpdateAdGroupCommandHandler : IRequestHandler<UpdateAdGroupCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public UpdateAdGroupCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdGroupCommand request)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<AdGroupEntity>(request);
            await AdGroupDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupDbRepo.SaveChangesAsync();

            return result.Success(true, entity.Id, entity);
        }
    }
}
