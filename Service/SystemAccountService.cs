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

public class SystemAccountService : ISystemAccountService
{
    public bool DeleteSystemAccount(SystemAccount account)
        => SystemAccountDAO.DeleteSystemAccount(account);

    public SystemAccount GetSystemAccount(short id)
        => SystemAccountDAO.GetSystemAccountByID(id);

    public List<SystemAccount> GetSystemAccounts()
        => SystemAccountDAO.GetSystemAccounts();

    public bool SaveSystemAccount(SystemAccountRequest account)
    {
        SystemAccount creatingAccount = new SystemAccount() 
        {
            AccountId = account.AccountId,
            AccountName = account.AccountName,
            AccountEmail = account.AccountEmail,
            AccountPassword = account.AccountPassword,
            AccountRole = account.AccountRole
        };
        return SystemAccountDAO.CreateSystemAccount(creatingAccount);
    }
        

    public bool UpdateSystemAccount(SystemAccount account)
        => SystemAccountDAO.UpdateSystemAccount(account);

    public int GetNewId()
        => SystemAccountDAO.GetNewId();

    public SystemAccount Login(string username, string password)
        => SystemAccountDAO.Login(username, password);
}
