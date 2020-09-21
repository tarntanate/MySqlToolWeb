using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherMappingProfile : Profile
    {
        public UpdatePublisherMappingProfile()
        {
            CreateMap<UpdatePublisherCommand, PublisherEntity>();
        }
    }
}
