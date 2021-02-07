using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.Orders;
using Shop.Database;

namespace Shop.UI.Pages.Checkout
{
    public class PaymentModel : PageModel
    {
        private ApplicationDbContext _ctx;
        private UserManager<IdentityUser> _userManager;

        public PaymentModel(ApplicationDbContext ctx, UserManager<IdentityUser> userManager)
        {
            _ctx = ctx;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            var information = new GetCustomerInformation(HttpContext.Session).Do();

            if (information == null)
            {
                return RedirectToPage("/Checkout/CustomerInformation");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            var CardOrder = new Application.Cart.GetOrder(HttpContext.Session, _ctx).Do();

            var sessionId = HttpContext.Session.Id;

            await new CreateOrder(_ctx).Do(new CreateOrder.Request
            {
                SessionId = sessionId,

                FirstName = CardOrder.CustomerInformation.FirstName,
                LastName = CardOrder.CustomerInformation.LastName,
                Email = CardOrder.CustomerInformation.Email,
                PhoneNumber = CardOrder.CustomerInformation.PhoneNumber,
                Address1 = CardOrder.CustomerInformation.Address1,
                Address2 = CardOrder.CustomerInformation.Address2,
                City = CardOrder.CustomerInformation.City,
                PostCode = CardOrder.CustomerInformation.PostCode,
                UserId = _userManager.GetUserId(User),

                Stocks = CardOrder.Products.Select(x => new CreateOrder.Stock
                {
                    StockId = x.StockId,
                    Qty = x.Qty
                }).ToList()
            });
            
            return RedirectToPage("/Index");
        }
    }
}
