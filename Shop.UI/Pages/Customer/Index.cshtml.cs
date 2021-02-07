using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.OrdersCustomer;
using Shop.Database;
using Shop.Domain.Enums;

namespace Shop.UI.Pages.Customer
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _ctx;
        private UserManager<IdentityUser> _userManager;

        public IndexModel(ApplicationDbContext ctx, UserManager<IdentityUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public IEnumerable<GetOrders.OrdersViewModel> Order { get; set; }


        public void OnGet()
        {
        
           var userId = _userManager.GetUserId(User);
            Order = new GetOrders(_ctx).Do(userId);
          

        }

       
    }
}
