using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Staff.PersonalManagement;
[Authorize(Roles = "1")]
public class NewsHistoryModel : PageModel
{

    private readonly INewsArticleService _newsArticleRepository;
    public List<NewsArticle> newsArticles { get; set; }

    [BindProperty]
    public NewsArticle NewArticle { get; set; }

    public NewsHistoryModel()
    {
        _newsArticleRepository = new NewsArticleService();
    }
    public IActionResult OnGet()
    {
        var userId = GetCurrentAccountId();
        newsArticles = _newsArticleRepository.GetNewArticleByAccID(userId);

        if (newsArticles == null)
        {
            return NotFound();
        }

        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Add the new article to the database
        //await _newsArticleRepository.CreateNewsArticle(NewArticle, )

        return RedirectToPage();
    }

    public short GetCurrentAccountId()
    {
        return short.Parse(User.FindFirst("UserId").Value); 
    }
}
