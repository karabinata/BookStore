namespace BookStore.Web.Infrastructure.Extensions
{
    public static class StringExtentions
    {
        private const string NumberFormat = "F2";

        public static string ToBgnPrice(this decimal priceText)
            => $"{priceText.ToString(NumberFormat)} лв.";
    }
}
