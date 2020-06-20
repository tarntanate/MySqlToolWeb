using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, HttpResult<long>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public CreateCampaignCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<HttpResult<long>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await CreateOnDb(request);
            return result;
        }

        private async Task<HttpResult<long>> CreateOnDb(CreateCampaignCommand request)
        {
            var result = new HttpResult<long>();

            var entity = Mapper.Map<CampaignEntity>(request);
            await CampaignDbRepo.InsertAsync(entity);
            await CampaignDbRepo.SaveChangesAsync();

            return result.Success(entity.Id);
        }
    }
}
