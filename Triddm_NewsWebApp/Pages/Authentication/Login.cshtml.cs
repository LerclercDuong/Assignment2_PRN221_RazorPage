using BusinessObjects.Request;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository;
using Repository.Interface;
using System.Security.Claims;

namespace Triddm_NewsWebApp.Pages.Authentication;
public class LoginModel : PageModel
{
    private readonly ISystemAccountService _systemAccountRepository;
    private IConfiguration _configuration;
    [BindProperty] public SignInRequest SignInRequest { get; set; }
    public string Message { get; set; }
    public LoginModel()
    {
        _systemAccountRepository = new SystemAccountService();
        _configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();

    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        var email = _configuration["AdminCredentials:Email"];
        var password = _configuration["AdminCredentials:Password"];

        var isAdmin = SignInRequest.email == email && SignInRequest.password == password;
        if (isAdmin)
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, SignInRequest.email),
                    new Claim(ClaimTypes.Role, "2") // Assume Admin role is 2
                };

            var identity = new ClaimsIdentity(claims, "login");

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToPage("/Admin/Index");
        }

        var account = _systemAccountRepository.Login(SignInRequest.email, SignInRequest.password);
        if (account == null)
        {
            Message = "Invalid username or password.";
            return Page();
        }
        else
        {
            var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, account.AccountEmail),
                    new Claim(ClaimTypes.Role, account.AccountRole.ToString()),
                    new Claim("UserId", account.AccountId.ToString()) // Add a custom claim for the user ID
                };

            var identity = new ClaimsIdentity(claims, "login");

            await HttpContext.SignInAsync(new ClaimsPrincipal(identity));

            return RedirectToPage("/Staff/Index");
        }

    }
}
