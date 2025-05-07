using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Web.Lib;
using Web.Lib.Services.Spec;

namespace Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService AccountService;
        private readonly ISessionService SessionService;
        public LoginModel(IAccountService accs, ISessionService ss)
        {
            AccountService = accs;
            SessionService = ss;
        }

        [BindProperty]
        [Display(Name = "Username", Prompt = "jsmith")]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "hunter2")]
        [Required]
        public string Password { get; set; }


        public async Task<ActionResult> OnGet()
        {
            if (User.IsAuthenticated())
            {
                return Redirect("~/");
            }
            if(await AccountService.NoAccountsExistAsync())
            {
                return Redirect("Setup");
            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (User.IsAuthenticated())
            {
                return Redirect("~/");
            }
            var result = await SessionService.LoginAsync(Username, Password);
            if(result is null)
            {
                return Page();
            }
            else
            {
                return Redirect("~/");
            }
        }
    }
}
