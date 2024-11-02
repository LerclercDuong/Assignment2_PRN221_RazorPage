using BusinessObjects;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface;

public interface INewsArticleService
{
    public List<NewsArticle> GetNewsArticlesByDate(DateTime startingTime, DateTime endTime);
    public List<NewsArticle> GetNewArticleByAccID(short accID);
    public List<NewsArticle> GetNewArticles();
    public void UpdateNewArticles(NewsArticle news);
    public void CreateNewsArticle(NewsArticle newsArticle, ICollection<Tag> tags);
}
