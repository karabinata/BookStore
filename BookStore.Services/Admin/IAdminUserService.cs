using BookStore.Services.Admin.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Admin
{
    public interface IAdminUserService
    {
        Task<IEnumerable<AdminUserListingServiceModel>> AllAsync();
    }
}
