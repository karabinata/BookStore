using System.Threading.Tasks;

namespace BookStore.Services.Orders
{
    public interface IOrderService
    {
        Task<bool> OrderItemAsync(string userId, int bookId);
    }
}
