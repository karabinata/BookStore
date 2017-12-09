using System.Collections.Generic;
using BookStore.Services.Admin.Models;
using BookStore.Data;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Services.Admin.Implementations
{
    public class AdminUserService : IAdminUserService
    {
        private readonly BookStoreDbContext db;

        public AdminUserService(BookStoreDbContext db)
        {
            this.db = db;
        }

        public async Task<IEnumerable<AdminUserListingServiceModel>> AllAsync()
            => await this.db
                .Users
                .OrderByDescending(u => u.RegistrationDate)
                .ProjectTo<AdminUserListingServiceModel>()
                .ToListAsync();
    }
}
