using BookStore.Services.Orders.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderListingServiceModel>> AllAsync(string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 4);

        Task<IEnumerable<OrderListingServiceModel>> MyOrdersAsync(string userId, string orderBy = "Id", string orderDirection = "descending", int page = 1, int pageSize = 4);

        Task<OrderDetailsServiceModel> DetailsAsync(int orderId);

        Task<bool> OrderBookAsync(string customerId, IEnumerable<int> bookIds, decimal totalPrice, Dictionary<int, int> itemQuantities);

        Task<bool> UnorderBookAsync(string userId, int bookId);

        Task<bool> IsOrdered(string userId, int bookId);

        Task<int> TotalAsync();
    }
}
