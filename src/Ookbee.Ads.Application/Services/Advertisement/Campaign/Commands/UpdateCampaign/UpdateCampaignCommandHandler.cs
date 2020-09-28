using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, Response<bool>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<CampaignEntity> CampaignDbRepo;

        public UpdateCampaignCommandHandler(
            IMapper mapper,
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            Mapper = mapper;
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<bool>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<CampaignEntity>(request);
            await CampaignDbRepo.UpdateAsync(entity.Id, entity);
            await CampaignDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
