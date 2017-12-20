using System.Threading.Tasks;
using BookStore.Services.Users.Models;
using BookStore.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Users.Implementations
{
    public class UserService : IUserService
    {
        private readonly BookStoreDbContext db;

        public UserService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<UserDetailsServiceModel> Details(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDetailsServiceModel>()
                .FirstOrDefaultAsync();
    }
}
