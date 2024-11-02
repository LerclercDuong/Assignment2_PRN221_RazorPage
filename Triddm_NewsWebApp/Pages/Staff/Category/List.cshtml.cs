using BusinessObjects;
using BusinessObjects.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Staff.Category;
[Authorize(Roles = "1")]
public class ListModel : PageModel
{
    private readonly ICategoryService _categoryRepository;

    public List<BusinessObjects.Entities.Category> categories { get; set; }

    [BindProperty]
    public short CategoryId { get; set; }

    public ListModel(ICategoryService categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }
    
    public void OnGet()
    {
        categories = _categoryRepository.GetCategories();
    }

    public IActionResult OnPostDelete()
    {

        BusinessObjects.Entities.Category category = _categoryRepository.GetCategory(CategoryId);
        _categoryRepository.DeleteCategory(category);
        return RedirectToPage("/staff/category/list");
    }
}
