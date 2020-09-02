using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.CreateCampaign
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
            var entity = Mapper.Map<CampaignEntity>(request);
            await CampaignDbRepo.InsertAsync(entity);
            await CampaignDbRepo.SaveChangesAsync(cancellationToken);

            var result = new HttpResult<long>();
            return result.Success(entity.Id);
        }
    }
}
