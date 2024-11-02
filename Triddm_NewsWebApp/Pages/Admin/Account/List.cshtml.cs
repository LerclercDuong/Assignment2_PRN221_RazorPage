using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Admin.Account;
[Authorize(Roles = "2")]
public class ListModel : PageModel
{
    private readonly ISystemAccountService systemAccountRepository;
    public List<SystemAccount> Accounts { get; set; }
    [BindProperty]
    public short AccountId { get; set; }
    public ListModel()
    {
        systemAccountRepository = new SystemAccountService();
    }
    public void OnGet()
    {
        LoadSystemAccounts();
    }
    private void LoadSystemAccounts()
    {
        List<SystemAccount> systemAccounts = systemAccountRepository.GetSystemAccounts();
        Accounts = systemAccounts;
    }

    public IActionResult OnPostDelete()
    {
        SystemAccount existingAccount = systemAccountRepository.GetSystemAccount(AccountId);
        systemAccountRepository.DeleteSystemAccount(existingAccount);
        return RedirectToPage("./List");
    }
}
