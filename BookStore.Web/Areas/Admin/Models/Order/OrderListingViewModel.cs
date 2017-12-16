using BookStore.Services.Orders.Models;
using System;
using System.Collections.Generic;

namespace BookStore.Web.Areas.Admin.Models.Order
{
    public class OrderListingViewModel
    {
        public IEnumerable<OrderListingServiceModel> Orders { get; set; }

        public int TotalOrders { get; set; }

        public int PageSize { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)this.TotalOrders / this.PageSize);

        public int CurrentPage { get; set; }

        public int NextPage => this.CurrentPage == this.TotalPages ? this.TotalPages : this.CurrentPage + 1;

        public int PreviousPage => this.CurrentPage <= 1 ? 1 : this.CurrentPage - 1;
    }
}
