using MediatR;
using Ookbee.Ads.Common.Response;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.IsExistsPublisherByName
{
    public class IsExistsPublisherByNameQuery : IRequest<Response<bool>>
    {
        public string Name { get; private set; }
        public string CountryCode { get; private set; }

        public IsExistsPublisherByNameQuery(string name, string countryCode)
        {
            Name = name;
            CountryCode = countryCode;
        }
    }
}
