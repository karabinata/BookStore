namespace BookStore.Web.Infrastructure.Extensions
{
    public static class StringExtentions
    {
        private const string NumberFormat = "F2";

        public static string ToBgnPrice(this decimal priceText)
        {
            return $"{priceText.ToString(NumberFormat)} лв.";
        }
    }
}
