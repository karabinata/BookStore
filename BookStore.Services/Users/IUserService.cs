using BookStore.Services.Users.Models;
using System.Threading.Tasks;

namespace BookStore.Services.Users
{
    public interface IUserService
    {
        Task<UserDetailsServiceModel> Details(string id);
    }
}
