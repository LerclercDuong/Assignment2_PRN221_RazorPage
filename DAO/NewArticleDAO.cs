using BusinessObjects;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer;

public class NewArticleDAO
{
    public static List<NewsArticle> GetNewArticleByAccID(int id)
    {
        List<NewsArticle> list = new List<NewsArticle>();
        try
        {
            using var dbContext = new NewsManagementDbContext();
            list = dbContext.NewsArticles.Where(o => o.CreatedById == id).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static void RemoveSystemAccountID(List<NewsArticle> list)
    {
        foreach (var item in list)
        {
            using var dbContext = new NewsManagementDbContext();
            item.CreatedById = null;
            dbContext.NewsArticles.Update(item);
            dbContext.SaveChanges();
        }
    }

    public static List<NewsArticle> GetNewsArticlesInTime(DateTime startingTime, DateTime endingTime)
    {
        List<NewsArticle> list = new List<NewsArticle>();
        try
        {
            using var dbContext = new NewsManagementDbContext();
            list = dbContext.NewsArticles
                .Where(o => (o.CreatedDate > startingTime && o.CreatedDate < endingTime))
                .OrderBy(o => o.CreatedDate)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static List<NewsArticle> GetNewsArticlesByCategory(int categoryId)
    {
        List<NewsArticle> list = new List<NewsArticle>();
        try
        {
            using var dbContext = new NewsManagementDbContext();
            list = dbContext.NewsArticles.Where(o => o.CategoryId == categoryId).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static List<NewsArticle> GetNewsArticles()
    {
        List<NewsArticle> list = new List<NewsArticle>();
        try
        {
            using var dbContext = new NewsManagementDbContext();
            list = dbContext.NewsArticles
                .Include(na => na.Tags)
                .ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return list;
    }

    public static NewsArticle GetNewsArticle(string id)
    {
        NewsArticle newsArticle = null;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            newsArticle = dbContext.NewsArticles
                .FirstOrDefault(o => o.NewsArticleId == id);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return newsArticle;
    }

    public static void CreateNewsArticle(NewsArticle news, ICollection<Tag> tags)
    {
        using var context = new NewsManagementDbContext();
        using var transaction = context.Database.BeginTransaction();

        try
        {
            foreach (var newTag in tags)
            {
                var existingTag = context.Tags.FirstOrDefault(t => t.TagName == newTag.TagName);
                if (existingTag != null)
                {
                    // Tag already exists, associate it with the news article
                    newTag.TagId = existingTag.TagId; // Ensure newTag has the correct TagId
                    existingTag.NewsArticles.Add(news);
                }
                else
                {
                    // Tag does not exist, add it to the database and associate it with the news article
                    context.Tags.Add(newTag);
                }
            }

            // Add the news article
            context.NewsArticles.Add(news);
            context.SaveChanges();

            // Commit the transaction
            transaction.Commit();
        }
        catch (Exception ex)
        {
            // Rollback the transaction if an exception occurs
            transaction.Rollback();
            throw new Exception("Error creating news article: " + ex.Message);
        }
    }

    public static void UpdateNewsArticle(NewsArticle news)
    {
        using var context = new NewsManagementDbContext();
        var existingArticle = context.NewsArticles.Include(a => a.Tags).SingleOrDefault(a => a.NewsArticleId == news.NewsArticleId);
        if (existingArticle != null)
        {
            // Update news article properties
            existingArticle.NewsTitle = news.NewsTitle;
            existingArticle.NewsContent = news.NewsContent;
            existingArticle.CategoryId = news.CategoryId;

            // Update associated tags
            var newTagIds = news.Tags.Select(t => t.TagId);
            var tagsToRemove = existingArticle.Tags.Where(t => !newTagIds.Contains(t.TagId)).ToList();
            foreach (var tagToRemove in tagsToRemove)
            {
                existingArticle.Tags.Remove(tagToRemove);
            }
            foreach (var newTag in news.Tags)
            {
                if (!existingArticle.Tags.Any(t => t.TagId == newTag.TagId))
                {
                    var existingTag = context.Tags.Find(newTag.TagId);
                    if (existingTag != null)
                    {
                        existingArticle.Tags.Add(existingTag);
                    }
                    else
                    {
                        existingArticle.Tags.Add(newTag);
                    }
                }
            }

            context.SaveChanges();
        }
    }

    public void DeleteNewsArticle(string newsArticleId)
    {
        try
        {
            using var context = new NewsManagementDbContext();
            var newsArticle = context.NewsArticles
                                     .SingleOrDefault(n => n.NewsArticleId.ToLower().Equals(newsArticleId.ToLower()));

            if (newsArticle != null)
            {
                // Soft deleting
                newsArticle.NewsStatus = false;
                context.NewsArticles.Update(newsArticle);
                context.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting news article: " + ex.Message);
        }
    }
}
