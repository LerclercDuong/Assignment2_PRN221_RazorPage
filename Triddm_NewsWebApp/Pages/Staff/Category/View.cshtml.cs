using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Staff.Category;
[Authorize(Roles = "1")]
public class ViewModel : PageModel
{
    private readonly ICategoryService _categoryRepository;

    [BindProperty]
    public BusinessObjects.Entities.Category category { get; set; }

    public ViewModel(ICategoryService categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public void OnGet(short id)
    {
        category = _categoryRepository.GetCategory(id);
    }

    public IActionResult OnPostUpdate()
    {
        _categoryRepository.UpdateCategory(category);
        return RedirectToPage("./List");
    }
}
