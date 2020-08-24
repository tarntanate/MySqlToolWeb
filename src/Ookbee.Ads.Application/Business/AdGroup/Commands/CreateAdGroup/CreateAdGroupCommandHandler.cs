using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.Commands.CreateUnitListByGroupId;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroup.Commands.CreateAdGroup
{
    public class CreateAdGroupCommandHandler : IRequestHandler<CreateAdGroupCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupEntity> AdGroupDbRepo { get; }

        public CreateAdGroupCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupEntity> adGroupDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupDbRepo = adGroupDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdGroupCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdGroupCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<AdGroupEntity>(request);
            await AdGroupDbRepo.InsertAsync(entity);
            await AdGroupDbRepo.SaveChangesAsync();

            await Mediator.Send(new CreateUnitListByGroupIdCommand(entity.Id));

            return result.Success(entity.Id, entity.Id, entity);
        }
    }
}
