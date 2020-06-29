using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdUnitType.Commands.CreateAdUnitType
{
    public class CreateAdUnitTypeCommandHandler : IRequestHandler<CreateAdUnitTypeCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdUnitTypeEntity> AdUnitTypeDbRepo { get; }

        public CreateAdUnitTypeCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdUnitTypeEntity> adUnitTypeDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdUnitTypeDbRepo = adUnitTypeDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdUnitTypeCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdUnitTypeCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<AdUnitTypeEntity>(request);
            await AdUnitTypeDbRepo.InsertAsync(entity);
            await AdUnitTypeDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
