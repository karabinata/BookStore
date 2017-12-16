using BookStore.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookStore.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService users;

        public UsersController(IUserService users)
        {
            this.users = users;
        }

        public async Task<IActionResult> Profile(string username)
        {
            var profile = await this.users.ProfileAsync(username);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }
    }
}
