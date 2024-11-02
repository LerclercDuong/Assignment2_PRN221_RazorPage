using BusinessObjects;
using BusinessObjects.Entities;
using BusinessObjects.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface;

public interface ICategoryService
{
    public List<Category> GetCategories();
    public Category GetCategory(short id);
    public bool SaveCategory(CategoryRequest category);
    public bool UpdateCategory(Category category);
    public bool DeleteCategory(Category account);
    public int GetNewId();
}
