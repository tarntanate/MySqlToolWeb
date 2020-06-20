using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Result;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;

namespace Ookbee.Ads.Application.Business.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, HttpResult<bool>>
    {
        private IMapper Mapper { get; }
        private IMediator Mediator { get; }
        private AdsDbRepository<CampaignEntity> CampaignDbRepo { get; }

        public UpdateCampaignCommandHandler(
            IMapper mapper,
            IMediator mediator,
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            Mapper = mapper;
            Mediator = mediator;
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<HttpResult<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var result = await UpdateOnDb(request);
            return result;
        }

        private async Task<HttpResult<bool>> UpdateOnDb(UpdateCampaignCommand request)
        {
            var result = new HttpResult<bool>();

            var entity = Mapper.Map<CampaignEntity>(request);
            await CampaignDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignDbRepo.SaveChangesAsync();

            return result.Success(true);
        }
    }
}
