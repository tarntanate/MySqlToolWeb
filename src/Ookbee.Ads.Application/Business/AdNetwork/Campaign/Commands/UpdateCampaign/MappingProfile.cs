using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Campaign.Commands.UpdateCampaign
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdateCampaignCommand, CampaignEntity>();
        }
    }
}
