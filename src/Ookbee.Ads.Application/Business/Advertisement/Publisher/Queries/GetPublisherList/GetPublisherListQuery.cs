using MediatR;
using Ookbee.Ads.Common.Response;
using System.Collections.Generic;

namespace Ookbee.Ads.Application.Business.Advertisement.Publisher.Queries.GetPublisherList
{
    public class GetPublisherListQuery : IRequest<Response<IEnumerable<PublisherDto>>>
    {
        public int Start { get; set; }

        public int Length { get; set; }

        public GetPublisherListQuery(int start, int length)
        {
            Start = start;
            Length = length;
        }
    }
}
