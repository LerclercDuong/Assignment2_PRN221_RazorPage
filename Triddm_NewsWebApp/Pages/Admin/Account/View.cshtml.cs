using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Admin.Account;
[Authorize(Roles = "2")]
public class ViewModel : PageModel
{
    private readonly ISystemAccountService _systemAccountRepository;
    [BindProperty]
    public SystemAccount Account { get; set; }
    public ViewModel()
    {
        _systemAccountRepository = new SystemAccountService();
    }
    public void OnGet(short id)
    {
        Account = _systemAccountRepository.GetSystemAccount(id);
    }
    public IActionResult OnPostUpdate()
    {
        _systemAccountRepository.UpdateSystemAccount(Account);
        return RedirectToPage("./List");
    }
}
