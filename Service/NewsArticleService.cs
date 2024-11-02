using BusinessObjects;
using BusinessObjects.Entities;
using DataAccessLayer;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class NewsArticleService : INewsArticleService
{
    public void CreateNewsArticle(NewsArticle newsArticle, ICollection<Tag> tags)
     => NewArticleDAO.CreateNewsArticle(newsArticle, tags);

    public List<NewsArticle> GetNewArticleByAccID(short accID)
    => NewArticleDAO.GetNewArticleByAccID(accID);

    public List<NewsArticle> GetNewArticles()
        => NewArticleDAO.GetNewsArticles();

    public List<NewsArticle> GetNewsArticlesByDate(DateTime startingTime, DateTime endTime)
        => NewArticleDAO.GetNewsArticlesInTime(startingTime, endTime);

    public void UpdateNewArticles(NewsArticle news)
        => NewArticleDAO.UpdateNewsArticle(news);
}
