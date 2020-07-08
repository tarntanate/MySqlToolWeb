using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Ad.Commands.UpdateAdStatus
{
    public class UpdateAdStatusCommandHandler : IRequestHandler<UpdateAdStatusCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdEntity> AdDbRepo { get; }

        public UpdateAdStatusCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdEntity> adDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdDbRepo = adDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateAdStatusOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateAdStatusOnDb(UpdateAdStatusCommand request)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<AdEntity>(request);
            await AdDbRepo.UpdateAsync(entity.Id, entity, x => x.Status);
            await AdDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
