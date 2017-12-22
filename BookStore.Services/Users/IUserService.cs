using BookStore.Services.Models;
using BookStore.Services.Users.Models;
using System.Threading.Tasks;

namespace BookStore.Services.Users
{
    public interface IUserService
    {
        Task<UserDetailsServiceModel> DetailsAsync(string id);

        Task<UserProfileServiceModel> ProfileAsync(string username);

        Task UpdateOrderInfoAsync(string userId, string address, string city, string phoneNumber);
    }
}
