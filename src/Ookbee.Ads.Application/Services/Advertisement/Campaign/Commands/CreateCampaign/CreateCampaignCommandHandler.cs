using AutoMapper;
using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.Campaign.Commands.CreateCampaign
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, Response<long>>
    {
        private readonly IMapper Mapper;
        private readonly AdsDbRepository<CampaignEntity> CampaignDbRepo;

        public CreateCampaignCommandHandler(
            IMapper mapper,
            AdsDbRepository<CampaignEntity> campaignDbRepo)
        {
            Mapper = mapper;
            CampaignDbRepo = campaignDbRepo;
        }

        public async Task<Response<long>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var entity = Mapper.Map<CampaignEntity>(request);
            await CampaignDbRepo.InsertAsync(entity);
            await CampaignDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<long>().Success(entity.Id);
        }
    }
}
