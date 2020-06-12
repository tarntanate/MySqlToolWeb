using AgileObjects.AgileMapper;
using MediatR;
using Ookbee.Ads.Application.Business.Advertiser.Queries.IsExistsAdvertiserByName;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertiser.Commands.CreateAdvertiser
{
    public class CreateAdvertiserCommandHandler : IRequestHandler<CreateAdvertiserCommand, HttpResult<long>>
    {
        private IMediator Mediator { get; }
        private AdsDbRepository<AdvertiserEntity> AdvertiserDbRepo { get; }

        public CreateAdvertiserCommandHandler(
            IMediator mediator,
            AdsDbRepository<AdvertiserEntity> advertiserDbRepo)
        {
            Mediator = mediator;
            AdvertiserDbRepo = advertiserDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateAdvertiserCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateAdvertiserCommand request)
        {
            var result = new HttpResult<long>();

            var isExistsAdvertiserByName = await Mediator.Send(new IsExistsAdvertiserByNameQuery(request.Name));
            if (isExistsAdvertiserByName.Ok)
                return result.Fail(409, $"Advertiser '{request.Name}' already exists.");

            var entity = Mapper
                .Map(request)
                .ToANew<AdvertiserEntity>();

            await AdvertiserDbRepo.InsertAsync(entity);
            await AdvertiserDbRepo.SaveChangesAsync();
            
            return result.Success(entity.Id);
        }
    }
}
