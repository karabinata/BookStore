using System.Threading.Tasks;
using BookStore.Services.Models;
using BookStore.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly BookStoreDbContext db;

        public UserService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<UserProfileServiceModel> ProfileAsync(string username)
            => await this.db
                .Users
                .Where(u => u.UserName == username)
                .ProjectTo<UserProfileServiceModel>()
                .FirstOrDefaultAsync();
    }
}
