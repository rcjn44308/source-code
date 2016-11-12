using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ML.Web.Models;
using PagedList;
using ML.Web.Helpers;

namespace ML.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceFactory serviceFactory;

        public HomeController(IServiceFactory serviceFactory)
        {
            this.serviceFactory = serviceFactory;
        }

        [Route("")]
        [Authorize]
        public ActionResult Index(HomeViewModel model)
        {
            if (!User.Identity.IsAuthenticated)
                RedirectToRoute("login");

            string[] values = User.Identity.Name.Split('|');

            model.username = string.IsNullOrEmpty(values[0].Trim()) ? "Impossible please re-login." : values[0].Trim();
            model.location = string.IsNullOrEmpty(values[5].Trim()) ? "Unable to get location." : values[5].Trim();
            model.longhitude = string.IsNullOrEmpty(values[3].Trim()) ? "Unable to get longhitude." : values[3].Trim();
            model.latitude = string.IsNullOrEmpty(values[4].Trim()) ? "Unable to get latitude." : values[4].Trim();
            model.contactNo = string.IsNullOrEmpty(values[8].Trim()) ? "Not Available." : values[8].Trim();

            #region Decrypt Cookies
            //HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

            //string cookiePath = ticket.CookiePath;
            //DateTime expiration = ticket.Expiration;
            //bool expired = ticket.Expired;
            //bool isPersistent = ticket.IsPersistent;
            //DateTime issueDate = ticket.IssueDate;
            //string name = ticket.Name;
            //string userData = ticket.UserData;
            //Int32 version = ticket.Version;
            #endregion

            return View(model);
        }

        [Route("mlx/generate-user-info")]
        [Authorize]
        public JsonResult GenerateUserInfo() {

            if (!User.Identity.IsAuthenticated)
                RedirectToRoute("login");

            string[] values = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.RefreshAccount refrsh = new OFW.Contracts.Models.RefreshAccount();
            refrsh.accountno = values[1].Trim(); ;
            refrsh.IsRefresh = 1;
            refrsh.UserName = values[0].Trim();

            ML.OFW.Services.OFW srv = new OFW.Services.OFW();
            var resp = srv.RefreshAccount(refrsh);

            bool hasPhoto = string.IsNullOrEmpty(resp.Photo) ? false : true;

            if (resp.respCode != "0") {
                return Json(new { status = true, hasImage = hasPhoto, response = resp }, JsonRequestBehavior.AllowGet);
            }
            return Json(new{ status = false, msg = resp.respMsg }, JsonRequestBehavior.AllowGet);
        }

        [Route("mlx/refresh-balance")]
        [Authorize]
        public JsonResult refBalance()
        {
            if (!User.Identity.IsAuthenticated)
                RedirectToRoute("login");

            string[] values = User.Identity.Name.Split('|');

            ML.OFW.Services.OFW srv = new OFW.Services.OFW();
            var resp = srv.refreshRunningBalance(values[1].Trim());

            if (resp.respCode != "0") {
                string balance = string.Format("{0:#,##0.00}", resp.runningBalance);
         
                return Json(new
                {
                    status = true,
                    respCode = "1",
                    msg = resp.respMsg,
                    RunBal = balance
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                respCode = "0",
                msg = resp.respMsg,
                RunBal = resp.runningBalance
            }, JsonRequestBehavior.AllowGet);
        }
    }
}