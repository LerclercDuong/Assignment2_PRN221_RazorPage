using BusinessObjects;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.VisualBasic;
using Repository;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Admin;

public class ReportArticleModel : PageModel
{
    private readonly INewsArticleService _newsArticleRepository;
    [BindProperty] public DateTime StartingTime { get; set; } = DateTime.Now.Date + TimeSpan.FromDays(-7);
    [BindProperty] public DateTime EndingTime { get; set; } = DateTime.Now.Date;
    public List<NewsArticle> newsArticles { get; set; }
    public ReportArticleModel()
    {
        _newsArticleRepository = new NewsArticleService();
    }
    public void OnGet()
    {
        newsArticles = _newsArticleRepository.GetNewsArticlesByDate(StartingTime, EndingTime);
    }
    public void OnPostGo()
    {
        newsArticles = _newsArticleRepository.GetNewsArticlesByDate(StartingTime, EndingTime);
    }
}
