using AutoMapper;
using Ookbee.Ads.Domain.Entities.AdsEntities;

namespace Ookbee.Ads.Application.Business.Publisher.Commands.UpdatePublisher
{
    public class UpdatePublisherMappingProfile : Profile
    {
        public UpdatePublisherMappingProfile()
        {
            CreateMap<UpdatePublisherCommand, PublisherEntity>();
        }
    }
}
