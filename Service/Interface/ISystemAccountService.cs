using BusinessObjects;
using BusinessObjects.Entities;
using BusinessObjects.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface;

public interface ISystemAccountService
{
    public SystemAccount Login(string username, string password);
    public List<SystemAccount> GetSystemAccounts();
    public SystemAccount GetSystemAccount(short id);
    public bool SaveSystemAccount(SystemAccountRequest account);
    public bool UpdateSystemAccount(SystemAccount account);
    public bool DeleteSystemAccount(SystemAccount account);
    public int GetNewId();
}
