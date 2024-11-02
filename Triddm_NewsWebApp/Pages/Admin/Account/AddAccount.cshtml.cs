using BusinessObjects;
using BusinessObjects.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Admin.Account;

[Authorize(Roles = "2")]
public class AddAccountModel : PageModel
{
    public readonly ISystemAccountService _systemAccountRepository;
    [BindProperty]
    public SystemAccountRequest accountRequest { get; set; }
    public AddAccountModel()
    {
        _systemAccountRepository = new SystemAccountService();
    }
    public void OnGet()
    {
        if (accountRequest == null)
        {
            accountRequest = new SystemAccountRequest();
        }
        accountRequest.AccountId = (short)_systemAccountRepository.GetNewId();
    }

    public void OnPost()
    {
        _systemAccountRepository.SaveSystemAccount(accountRequest);
    }
}
