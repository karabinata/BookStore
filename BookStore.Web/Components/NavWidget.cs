using BookStore.Data.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStore.Web.Components
{
    public class NavWidget : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = Enum.GetNames(typeof(Category));

            return View(categories);
        }
    }
}