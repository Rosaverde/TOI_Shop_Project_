using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Database;

namespace Shop.UI.Pages
{
    public class CartModel : PageModel
    {

        private ApplicationDbContext _ctx;

        public CartModel(ApplicationDbContext ctx)
        {
            _ctx = ctx;
        }


        public IEnumerable<GetCart.Response> Cart { get; set; }

        public IActionResult OnGet()
        {

        Cart = new GetCart(HttpContext.Session, _ctx).Do();
            int v = Cart.Count();
            if(v == 0)
            {
                return RedirectToPage("/Index");
            }

            return Page();
        }
    }
}
