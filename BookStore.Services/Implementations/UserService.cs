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
