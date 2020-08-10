using AutoMapper;
using MediatR;
using Ookbee.Ads.Application.Business.AdGroupItem.Queries.GetAdGroupItemById;
using Ookbee.Ads.Application.Business.AdNetwork.GroupItem.Commands.CreateGroupItemListByKey;
using Ookbee.Ads.Common.Extensions;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdGroupItem.Commands.UpdateAdGroupItem
{
    public class UpdateAdGroupItemCommandHandler : IRequestHandler<UpdateAdGroupItemCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<AdGroupItemEntity> AdGroupItemDbRepo { get; }

        public UpdateAdGroupItemCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<AdGroupItemEntity> adGroupItemDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            AdGroupItemDbRepo = adGroupItemDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateAdGroupItemCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateAdGroupItemCommand request)
        {
            var result = new HttpResult<bool>();

            var getAdGroupItem = await Mediator.Send(new GetAdGroupItemByIdQuery(request.Id));

            var entity = Mapper.Map<AdGroupItemEntity>(request);
            await AdGroupItemDbRepo.UpdateAsync(entity.Id, entity);
            await AdGroupItemDbRepo.SaveChangesAsync();

            await Mediator.Send(new CreateGroupItemListByKeyCommand(request.AdGroupId));

            if (getAdGroupItem.Ok && getAdGroupItem.Data.HasValue())
                await Mediator.Send(new CreateGroupItemListByKeyCommand(getAdGroupItem.Data.AdGroupId));

            return result.Success(true, entity.Id, entity);
        }
    }
}
