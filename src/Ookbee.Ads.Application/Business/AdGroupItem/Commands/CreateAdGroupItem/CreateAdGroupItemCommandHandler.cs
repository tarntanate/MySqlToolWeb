using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Commands.CreateGroupItemListByKey;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.CreateAdGroupItem
{
    public class CreateAdGroupItemCommandHandler : IRequestHandler<CreateAdGroupItemCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public CreateAdGroupItemCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdGroupItemCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdGroupItemCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<AdGroupItemEntity>(request);
            await AdGroupItemDbRepo.InsertAsync(entity);
            await AdGroupItemDbRepo.SaveChangesAsync();

            await Mediator.Send(new CreateGroupItemListByKeyCommand(request.AdGroupId));

            return result.Success(entity.Id, entity.Id, entity);
        }
    }
}
