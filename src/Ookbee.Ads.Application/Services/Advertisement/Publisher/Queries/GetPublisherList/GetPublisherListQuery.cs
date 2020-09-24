using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Services.Advertisement.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQuery : IRequest<Response<IEnumerable<PublisherDto>>>
    {
        public int Start { get; private set; }

        public int Length { get; private set; }

        public GetPublisherListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
