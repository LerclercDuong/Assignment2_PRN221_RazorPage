using BusinessObjects;
using BusinessObjects.Context;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer;

public class SystemAccountDAO
{
    public static List<SystemAccount> GetSystemAccounts()
    {
        List<SystemAccount> list = new List<SystemAccount>();
        try
        {
            using var dbContext = new NewsManagementDbContext();
            list = dbContext.SystemAccounts.ToList();
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return list;
    }

    public static SystemAccount GetSystemByEmail(string email)
    {
        SystemAccount account = null;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            account = dbContext.SystemAccounts.FirstOrDefault(o => o.AccountEmail == email);
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return account;
    }

    public static SystemAccount GetSystemAccountByID(short AccountId)
    {
        SystemAccount account = null;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            account = dbContext.SystemAccounts
                .FirstOrDefault(o => o.AccountId == AccountId);
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return account;
    }

    public static bool CreateSystemAccount(SystemAccount account)
    {
        bool successCheck = false;
        try
        {
            SystemAccount dupAcc = GetSystemAccountByID(account.AccountId);
            SystemAccount emailAcc = GetSystemByEmail(account.AccountEmail);
            if (dupAcc == null && emailAcc == null)
            {
                using var dbContext = new NewsManagementDbContext();
                dbContext.SystemAccounts.Add(account);
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

    public static bool UpdateSystemAccount(SystemAccount account)
    {
        bool successCheck = false;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            SystemAccount systemAccount = GetSystemAccountByID(account.AccountId);
            SystemAccount emailAcc = GetSystemByEmail(account.AccountEmail);
            bool check = false;
            if (systemAccount != null)
            {
                if (emailAcc != null)
                {
                    if (emailAcc.AccountId == account.AccountId)
                    {
                        check = true;
                    }
                }
                else
                {
                    check = true;
                }
                if (check)
                {
                    dbContext.SystemAccounts.Update(account);
                    dbContext.SaveChanges();
                    successCheck = true;
                }

            }
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return successCheck;
    }

    public static bool DeleteSystemAccount(SystemAccount account)
    {
        bool successCheck = false;

        try
        {
            using var dbContext = new NewsManagementDbContext();
            SystemAccount systemAccount = GetSystemAccountByID(account.AccountId);
            if (systemAccount != null)
            {
                List<NewsArticle> list = NewArticleDAO.GetNewArticleByAccID(account.AccountId);
                if (list != null)
                {
                    NewArticleDAO.RemoveSystemAccountID(list);
                }
                dbContext.SystemAccounts.Remove(account);
                dbContext.SaveChanges();
                successCheck = true;
            }
        }
        catch (Exception ex)
        {
            throw new Exception("SystemAccount: " + ex.Message);
        }
        return successCheck;
    }

    public static int GetNewId()
    {
        int newID;
        try
        {
            using var dbContext = new NewsManagementDbContext();
            newID = dbContext.SystemAccounts.Max(o => o.AccountId) + 1;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return newID;
    }

    public static SystemAccount Login(string email, string password)
    {
        SystemAccount acccount = null;
        try
        {
            acccount = GetSystemByEmail(email);
            if (acccount != null)
            {
                if (acccount.AccountPassword != password)
                {
                    acccount = null;
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        return acccount;
    }
}
