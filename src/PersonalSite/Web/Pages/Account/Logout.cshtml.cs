using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Lib.Services.Spec;

namespace Web.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly ISessionService SessionService;
        public LogoutModel(ISessionService ss)
        {
            SessionService = ss;
        }
        public async Task<ActionResult> OnGet()
        {
            await SessionService.LogoutAsync();
            return Redirect("~/");
        }
    }
}
