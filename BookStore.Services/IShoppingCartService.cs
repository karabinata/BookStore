using BookStore.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDetailsServiceModel>> Details(IEnumerable<int> itemIds);
    }
}
