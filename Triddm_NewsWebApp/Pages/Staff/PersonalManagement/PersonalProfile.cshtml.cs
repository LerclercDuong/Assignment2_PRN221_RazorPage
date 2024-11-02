using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Interface;
using System.Net.Mail;

namespace Triddm_NewsWebApp.Pages.Staff.PersonalManagement;

[Authorize(Roles = "1")]
public class PersonalProfileModel : PageModel
{
    private readonly ISystemAccountService _systemAccountRepository;
    [BindProperty]
    public string Name { get; set; }

    [BindProperty]
    public string Email { get; set; }

    public string Message { get; set; }
    private SystemAccount systemAccount { get; set; }
    public PersonalProfileModel()
    {
        _systemAccountRepository = new SystemAccountService();
        
    }

    public void OnGet()
    {
        // You can initialize data or perform any logic on page load
        systemAccount = _systemAccountRepository.GetSystemAccount(GetCurrentAccountId());
        Name = systemAccount.AccountName;
        Email = systemAccount.AccountEmail;
    }

    public IActionResult OnPost()
    {
        systemAccount = _systemAccountRepository.GetSystemAccount(GetCurrentAccountId());
        SystemAccount updateAccount = new SystemAccount()
        {
            AccountId = systemAccount.AccountId,
            AccountEmail = Email,
            AccountName = Name,
            AccountPassword = systemAccount.AccountPassword,
            AccountRole = systemAccount.AccountRole
        };
        _systemAccountRepository.UpdateSystemAccount(updateAccount);

        return Page();
    }

    public short GetCurrentAccountId()
    {
        return short.Parse(User.FindFirst("UserId").Value);
    }
}
