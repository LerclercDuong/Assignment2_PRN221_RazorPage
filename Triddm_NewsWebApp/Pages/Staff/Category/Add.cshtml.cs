using BusinessObjects.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Interface;

namespace Triddm_NewsWebApp.Pages.Staff.Category;
[Authorize(Roles = "1")]
public class AddModel : PageModel
{
    private readonly ICategoryService _categoryRepository;

    public AddModel(ICategoryService categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [BindProperty]
    public CategoryRequest categoryRequest {  get; set; }
    public void OnGet()
    {
    }

    public void OnPost()
    {
        _categoryRepository.SaveCategory(categoryRequest);
    }
}
