using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Commands.UpdatePublisher
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UpdatePublisherCommand, PublisherEntity>();
        }
    }
}
