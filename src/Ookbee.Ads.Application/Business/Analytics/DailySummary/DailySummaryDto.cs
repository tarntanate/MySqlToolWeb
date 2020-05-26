namespace Ookbee.Ads.Application.Business.Analytics.DailySummary
{
    public class DailySummaryDto
    {
        public string Id { get; set; }

        public int Requests { get; set; } = 0;

        public int Fills { get; set; } = 0;

        public int Impressions { get; set; } = 0;

        public int Clicks { get; set; } = 0;
    }
}
