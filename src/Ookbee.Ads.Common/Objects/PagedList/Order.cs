namespace Ookbee.Ads.Common.Objects.PagedList
{
    public class Order
    {
        public Order()
        {
            Ascending = true;
        }

        public bool Ascending { get; set; }

        public string Property { get; set; }
    }
}
