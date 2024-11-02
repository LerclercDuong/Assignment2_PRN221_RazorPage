using BusinessObjects;
using BusinessObjects.Context;
using BusinessObjects.Entities;

namespace DataAccessLayer;

public class CategoryDAO
{
    public static List<Category> GetCategories()
    {
        List<Category> list = new List<Category>();
        try
        {
            using var dbContext = new NewsManagementDbContext();
            list = dbContext.Categories.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("Category: " + ex.Message);
        }
        return list;
    }

    public static Category GetCategory(int id)
    {
        Category category = null;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            category = dbContext.Categories
                .FirstOrDefault(o => o.CategoryId == id);
        }
        catch (Exception ex)
        {
            throw new Exception("Category: " + ex.Message);
        }
        return category;
    }

    public static Category GetCategoryByName(string name)
    {
        Category category = null;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            category = dbContext.Categories
                .FirstOrDefault(o =>
                    o.CategoryName.ToLower() == name.ToLower());
        }
        catch (Exception ex)
        {
            throw new Exception("Category: " + ex.Message);
        }
        return category;
    }

    public static bool CreateCategory(Category category)
    {
        bool successCheck = false;
        try
        {
            Category dupName = GetCategoryByName(category.CategoryName);
            if (dupName == null)
            {
                using var dbContext = new NewsManagementDbContext();
                dbContext.Categories.Add(category);
                dbContext.SaveChanges();
                successCheck = true;
            }
            else
            {
                successCheck = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return successCheck;
    }
    public static bool UpdateCategory(Category category)
    {
        bool successCheck = false;
        try
        {
            Category dupCat = GetCategory(category.CategoryId);
            Category dupName = GetCategoryByName(category.CategoryName);
            bool flag = false;
            if (dupCat != null)
            {
                if (dupName != null)
                {
                    if (dupName.CategoryId == category.CategoryId)
                    {
                        flag = true;
                    }
                }
                else
                {
                    flag = true;
                }
                if (flag)
                {
                    using var dbContext = new NewsManagementDbContext();
                    dbContext.Categories.Update(category);
                    dbContext.SaveChanges();
                    successCheck = true;
                }

            }
            else
            {
                successCheck = false;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return successCheck;
    }

    public static bool DeleteCategory(Category category)
    {
        bool successCheck = false;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            Category curCategory = GetCategory(category.CategoryId);
            if (curCategory != null)
            {
                List<NewsArticle> list = NewArticleDAO.GetNewsArticlesByCategory(category.CategoryId);
                if (list.Count == 0)
                {
                    dbContext.Categories.Remove(category);
                    dbContext.SaveChanges();
                    successCheck = true;
                }
                else
                {
                    successCheck = false;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Category: " + ex.Message);
        }
        return successCheck;
    }

    public static int GetNewId()
    {
        int newID;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            newID = dbContext.Categories.Max(o => o.CategoryId) + 1;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return newID;
    }
}


