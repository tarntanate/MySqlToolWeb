using MediatR;
using Ookbee.Ads.Common.Response;
using Ookbee.Ads.Domain.Entities.AdsEntities;
using Ookbee.Ads.Persistence.EFCore.AdsDb;
using System.Threading;
using System.Threading.Tasks;

namespace Ookbee.Ads.Application.Services.Advertisement.AdGroupType.Commands.DeleteAdGroupType
{
    public class DeleteAdGroupTypeCommandHandler : IRequestHandler<DeleteAdGroupTypeCommand, Response<bool>>
    {
        private readonly AdsDbRepository<AdGroupTypeEntity> AdGroupTypeDbRepo;

        public DeleteAdGroupTypeCommandHandler(
            AdsDbRepository<AdGroupTypeEntity> adGroupTypeDbRepo)
        {
            AdGroupTypeDbRepo = adGroupTypeDbRepo;
        }

        public async Task<Response<bool>> Handle(DeleteAdGroupTypeCommand request, CancellationToken cancellationToken)
        {
            await AdGroupTypeDbRepo.DeleteAsync(request.Id);
            await AdGroupTypeDbRepo.SaveChangesAsync(cancellationToken);
            return new Response<bool>().OK(true);
        }
    }
}
