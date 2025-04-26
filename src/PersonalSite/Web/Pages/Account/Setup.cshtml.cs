using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Lib.Services.Spec;

namespace Web.Pages.Account
{
    public class SetupModel : PageModel
    {
        private readonly IAccountService AccountService;
        public SetupModel(IAccountService accs)
        {
            AccountService = accs;
        }

        [BindProperty]
        [Required]
        [MaxLength(64)]
        [Display(Name = "Login Username", Prompt = "jsmith")]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "hunter2")]
        public string Password { get; set; }
        [BindProperty]
        [MaxLength(64)]
        [Display(Name = "Display Name", Prompt = "John Smith")]
        public string DisplayName { get; set; }

        public async Task<ActionResult> OnGet()
        {
            if(!await AccountService.NoAccountsExistAsync())
            {
                return Redirect("Login");
            }
            return Page();
        }

        public async Task<ActionResult> OnPost()
        {
            if (!await AccountService.NoAccountsExistAsync())
            {
                return BadRequest();
            }
            await AccountService.SignUpAsync(Username, Password, DisplayName);
            return Redirect("~/");
        }
    }
}
