using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Lib.Services.Spec;

namespace Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IObjectViewService ObjectViewService;
        public IndexModel(IObjectViewService ovs)
        {
            ObjectViewService = ovs;
        }

        public async void OnGet()
        {
            await ObjectViewService.RecordPageViewAsync("Index");
        }
    }
}
