using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Lib.Services.Spec;

namespace Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService AccountService;
        public LoginModel(IAccountService accs)
        {
            AccountService = accs;
        }

        public async Task<ActionResult> OnGet()
        {
            if(await AccountService.NoAccountsExistAsync())
            {
                return Redirect("Setup");
            }
            return Page();
        }
    }
}
