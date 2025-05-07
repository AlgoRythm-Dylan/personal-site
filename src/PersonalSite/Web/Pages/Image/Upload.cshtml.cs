using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Image
{
    [Authorize]
    public class UploadModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}
