using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Business.Advertisement.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, Response<bool>>
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

        public async Task<Response<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<CampaignEntity>(request);
            await CampaignDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignDbRepo.SaveChangesAsync(cancellationToken);

            var result = new Response<bool>();
            return result.Success(true);
        }
    }
}
