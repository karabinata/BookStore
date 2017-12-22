using System.Threading.Tasks;
using BookStore.Services.Users.Models;
using BookStore.Data;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using BookStore.Services.Models;

namespace BookStore.Services.Users.Implementations
{
    public class UserService : IUserService
    {
        private readonly BookStoreDbContext db;

        public UserService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<UserDetailsServiceModel> DetailsAsync(string id)
            => await this.db
                .Users
                .Where(u => u.Id == id)
                .ProjectTo<UserDetailsServiceModel>()
                .FirstOrDefaultAsync();

        public async Task<UserProfileServiceModel> ProfileAsync(string username)
            => await this.db
                .Users
                .Where(u => u.UserName == username)
                .ProjectTo<UserProfileServiceModel>()
                .FirstOrDefaultAsync();

        public async Task UpdateOrderInfoAsync(string userId, string address, string city, string phoneNumber)
        {
            var user = await this.db.Users.FindAsync(userId);

            var userAddressIsChanged = user.Address != address;
            var userCityIsChanged = user.City != city;
            var userPhoneIsChanged = user.PhoneNumber != phoneNumber;

            if (userAddressIsChanged)
            {
                user.Address = address;
            }

            if (userCityIsChanged)
            {
                user.City = city;
            }

            if (userPhoneIsChanged)
            {
                user.PhoneNumber = phoneNumber;
            }

            if (userAddressIsChanged || userCityIsChanged || userPhoneIsChanged)
            {
                await this.db.SaveChangesAsync();
            }
        }
    }
}
