using BusinessObjects;
using BusinessObjects.Entities;
using BusinessObjects.Request;
using DataAccessLayer;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class CategoryService : ICategoryService
{
    public bool DeleteCategory(Category account)
        => CategoryDAO.DeleteCategory(account);

    public List<Category> GetCategories()
        => CategoryDAO.GetCategories();

    public Category GetCategory(short id)
        => CategoryDAO.GetCategory(id);

    public int GetNewId()
        => CategoryDAO.GetNewId();

    public bool SaveCategory(CategoryRequest category)
    {
        Category creatingCategory = new Category() 
        {
            CategoryName = category.CategoryName,
            CategoryDesciption = category.CategoryDesciption
        };
        return CategoryDAO.CreateCategory(creatingCategory);
    }

    public bool UpdateCategory(Category category)
        => CategoryDAO.UpdateCategory(category);
}
