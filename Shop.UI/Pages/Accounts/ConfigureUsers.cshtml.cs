using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Shop.Application.Cart;
using Shop.Application.UsersAdmin;

namespace Shop.UI.Pages.Accounts
{
    public class ConfigureUsers : PageModel
    {
        private UserManager<IdentityUser> _userManager;

        public ConfigureUsers(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }

        [BindProperty]
        public CreateUserViewModel Input { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var managerUser = new IdentityUser()
            {
                UserName = Input.Username
            };

            await _userManager.CreateAsync(managerUser, Input.Password);

            var managerClaim = new Claim("Role", "Customer");

            await _userManager.AddClaimAsync(managerUser, managerClaim);

            return RedirectToPage("/Index");
        }

        public class CreateUserViewModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string PasswordConfirmation { get; set; }
        }

    }


}
