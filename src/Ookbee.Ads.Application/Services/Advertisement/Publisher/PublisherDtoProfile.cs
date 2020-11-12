using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher
{
    public class PublisherDtoProfile : Profile
    {
        public PublisherDtoProfile()
        {
            CreateMap<PublisherEntity, PublisherDto>();
        }
    }
}
