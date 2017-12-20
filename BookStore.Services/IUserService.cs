using BookStore.Services.Models;
using System.Threading.Tasks;

namespace BookStore.Services
{
    public interface IUserService
    {
        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task UpdateOrderInfoAsync(string userId, string address, string city, string phoneNumber);
    }
}
