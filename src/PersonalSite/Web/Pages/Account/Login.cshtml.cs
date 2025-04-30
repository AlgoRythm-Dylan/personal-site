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
        public LoginModel(IAccountService accs)
        {
            AccountService = accs;
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
    }
}
