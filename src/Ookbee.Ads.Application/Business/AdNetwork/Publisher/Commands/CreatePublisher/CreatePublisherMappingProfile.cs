using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.AdNetwork.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherMappingProfile : Profile
    {
        public CreatePublisherMappingProfile()
        {
            CreateMap<CreatePublisherCommand, PublisherEntity>();
        }
    }
}
