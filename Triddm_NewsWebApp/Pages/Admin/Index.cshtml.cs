using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Triddm_NewsWebApp.Pages.Admin;
[Authorize(Roles = "2")]
public class IndexModel : PageModel
{
    public void OnGet()
    {
    }
}
