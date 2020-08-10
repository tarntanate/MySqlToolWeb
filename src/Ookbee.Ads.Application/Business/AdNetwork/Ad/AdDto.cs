namespace Ookbee.Ads.Application.Business.AdNetwork.Ad.Queries.GetAdByKeyQuery
{
    public class AdDto
    {
        public AdDataDto Ad { get; set; }

        public AdAnalyticsDto Analytics { get; set; }

        public string AdUnitType { get; set; }
    }
}
