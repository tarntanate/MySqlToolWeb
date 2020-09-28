using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Commands.CreatePublisher
{
    public class CreatePublisherMappingProfile : Profile
    {
        public CreatePublisherMappingProfile()
        {
            CreateMap<CreatePublisherCommand, PublisherEntity>();
        }
    }
}
