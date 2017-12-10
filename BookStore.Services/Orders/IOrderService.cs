using System.Threading.Tasks;

namespace BookStore.Services.Orders
{
    public interface IOrderService
    {
        Task<bool> OrderBookAsync(string userId, int bookId);

        Task<bool> UnorderBookAsync(string userId, int bookId);

        Task<bool> IsOrdered(string userId, int bookId);
    }
}
