
using Glimpse.AspNet.Tab;
using ML.Web.Models;
using System;
namespace ML.Web
{
    public class refreshAccountBalance
    {
        public String GetAccountBal( string AccountNo, string Ursname)
        {
            ML.OFW.Contracts.Models.RefreshAccount refrsh = new OFW.Contracts.Models.RefreshAccount();
            refrsh.accountno = AccountNo;
            refrsh.IsRefresh = 1;

            ML.OFW.Services.OFW srv = new OFW.Services.OFW();
            var resp = srv.RefreshAccount(refrsh);
            return resp.balance;
        }
    }
}