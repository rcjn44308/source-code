
using ML.Web.Models;
using System.Web.Mvc;
using ML.OFW.Contracts;
using System.Web;
using System;
using System.Web.Security;

namespace ML.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IServiceFactory _serviceFactory;
        private readonly IAuthorizationProvider _authorizationProvider;

        public UserController(IServiceFactory serviceFactory, IAuthorizationProvider authorizationProvider)
        {
            _serviceFactory = serviceFactory;
            _authorizationProvider = authorizationProvider;
        }

        [HttpGet]
        [Route("login")]
        public ActionResult Login(string q, string q1, string q2, string q3, string q4, string q5, string q6)
        {
            var model = new LoginViewModel
            {
                RedirectUrl = q,
                dvid = q1,
                cellNum = q2,
                longt = q3,
                lat = q4,
                loc = q5,
                vrsn = q6
            };

            if (TempData["model"] != null)
                model = TempData["model"] as LoginViewModel;

            return View("UserLogin", model);
        }

        [Route("mlx/check/version")]
        public JsonResult checkVersion(string q, string q1)
        {
            if (string.IsNullOrEmpty(q1))
            {
                q1 = "0";
            }
            if (!string.IsNullOrEmpty(q.Trim()))
            {
                ML.OFW.Contracts.Models.myVersion vrs = new OFW.Contracts.Models.myVersion();

                vrs.deviceID = q.Trim();
                vrs.toUpdate = false;
                vrs.version = q1.Trim();

                ML.OFW.Services.OFW service = new OFW.Services.OFW();

                var respVal = service.myVersion(vrs);

                if (respVal.isUpdated == true)
                    respVal.respMsg = "Up-to-date version!";
                else
                    respVal.respMsg = "Out-of-date version!";

                if (respVal.respCode == 0)
                {
                    return Json(new { respVal = respVal, errCode = "0", msg = respVal.respMsg }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { respVal = respVal, errCode = "0", msg = respVal.respMsg }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { errCode = "2000", msg = "Call Developer!!!" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Route("authenticate")]
        [ValidateAntiForgeryToken]
        public ActionResult Authenticate(LoginViewModel model)
        {
            string isSubAgent = string.Empty;
            string Longitude = string.Empty;
            string Latitude = string.Empty;
            string Location = string.Empty;
            string IsPartnerAllow = string.Empty;

            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrWhiteSpace(model.Username))
            {
                return Json(new { errCode = "0", msg = "The username field is required." }, JsonRequestBehavior.AllowGet);
            }
            if (string.IsNullOrEmpty(model.Password) || string.IsNullOrWhiteSpace(model.Password))
            {
                return Json(new { errCode = "0", msg = "The password field is required." }, JsonRequestBehavior.AllowGet);
            }

            ML.OFW.Contracts.Models.LoginModel loginService = new OFW.Contracts.Models.LoginModel();
            try
            {
                loginService.username = model.Username.Trim();
                loginService.password = model.Password.Trim();
                loginService.IMEI = model.dvid.Trim();
                loginService.MobileNo = model.cellNum.Trim();
                loginService.IsSendout = 1;
            }
            catch (System.Exception ex)
            {
                return Json(new { errCode = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            var service = _serviceFactory.GetService<IOFW>();
            var validLogin = service.Login(loginService);

            if (validLogin.respCode != "1")
            {
                string msg1 = "Your account has been locked out because of too many invalid kptn search attempt. Please contact the administrator to have your account unlocked.";

                if (validLogin.respMsg.Trim() == msg1.Trim())
                {
                    return Json(new { errCode = "1", msg = "You have reach your maximum KPTN search attempt. To unlock your account, please call our 24/7 Mlhuillier Support." }, JsonRequestBehavior.AllowGet);
                }

                string msg2 = "Your maximum login attempts exceeded. To unlock your account please call the administrator.";
                if (validLogin.respMsg.Trim() == msg2.Trim())
                {
                    return Json(new { errCode = "1", msg = "You have exceeded your login attempt. To unlock your account, please call our 24/7 Mlhuillier Support." }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { errCode = "0", msg = validLogin.respMsg }, JsonRequestBehavior.AllowGet);
            }

            try
            {
                isSubAgent = validLogin.IsSubAgent.Equals(true) ? "1" : "0";
                //string isSubAgent = "0";

                Longitude = model.longt.Equals("") ? "0" : model.longt;
                Latitude = model.lat.Equals("") ? "0" : model.lat;
                Location = model.loc;
            }
            catch (System.Exception ex)
            {
                return Json(new { errCode = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            // Username | Account No | Email Address | Longitude | Latitude | Location
            //string UserAut = model.Username + "|" + validLogin.info.accountno + "|" + validLogin.info.emailaddress + "|" + Longitude + "|" + Latitude + "|" + Location + "|" + isSubAgent + "|" + validLogin.info.balance;
            string UserAut = model.Username + "|"
                           + validLogin.info.accountno + "|"
                           + validLogin.info.emailaddress + "|"
                           + Longitude + "|" + Latitude + "|"
                           + Location + "|" + model.dvid + "|"
                           + isSubAgent + "|"
                           + model.cellNum + "|"
                           + 1;

            _authorizationProvider.SetAuthCookie(UserAut, true);

            #region Sample Auth
            //// 1st               
            ////_authorizationProvider.SetAuthCookie(UserAut, true);
            //var authTicket = new FormsAuthenticationTicket(
            //     1,
            //    "1st",  //user id
            //    DateTime.Now,
            //    DateTime.Now.AddMinutes(20),  // expiry
            //    true,  //true to remember
            //    "", //roles 
            //    "mlx-bills-pay/"
            //   );
            ////encrypt the ticket and add it to a cookie
            //HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
            //Response.Cookies.Add(cookie);

            //// 2st
            //FormsAuthenticationTicket _ticket = new FormsAuthenticationTicket(
            //    1,
            //    "SanpleData",  //user id
            //    DateTime.Now,
            //    DateTime.Now.AddMinutes(20),  // expiry
            //    true,  //true to remember
            //    "", //roles 
            //    "mlx-bills-pay/"
            //   );
            //string _encryptedTicket = FormsAuthentication.Encrypt(_ticket);
            //HttpCookie _cookie = new HttpCookie("1stsample", _encryptedTicket);
            //Response.Cookies.Add( _cookie);

            // 3nd
            FormsAuthenticationTicket _ticket1 = new FormsAuthenticationTicket(
               1,
               "SanpleData1 sadfdsf sdf sdf",  //user id
               DateTime.Now,
               DateTime.Now.AddMinutes(20),  // expiry
               true,  //true to remember
               "", //roles 
               "/"
              );
            string _encryptedTicket1 = FormsAuthentication.Encrypt(_ticket1);
            HttpCookie _cookie1 = new HttpCookie("2ndsample", _encryptedTicket1);
            Response.Cookies.Add(_cookie1);

            //string samapleurl = Url.RouteUrl("");

            //return Json(new { ok = true, action = samapleurl });
            #endregion

            return Json(new { ok = true, action = Url.RouteUrl("") });
        }

        [Route("logout")]
        [Authorize]
        public ActionResult Logout()
        {
            //Clear Authentication
            _authorizationProvider.SignOut();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string cookie in myCookies)
            {
                Response.Cookies[cookie].Expires = DateTime.Now.AddDays(-1);
            }
            Session.Abandon();
            return RedirectToAction("Login");
        }

        [Route("changepassword")]
        [Authorize]
        public ActionResult ChangePassword() {

            return View();
        }

        [Route("changepassword")]
        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(string oldPass, string newPass, string confirm)
        {
            string[] values = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.ChangePassword changePass = new OFW.Contracts.Models.ChangePassword();

            changePass.username = values[0].Trim();
            changePass.oldPassword = oldPass;
            changePass.newPassword = newPass;
            changePass.confirmNewPassword = confirm;
            changePass.is_subAgent = values[7].Trim().Equals("1") ? true : false;

            ML.OFW.Services.OFW respService = new OFW.Services.OFW();
            var valresp = respService.ChangePassword(changePass);

            if (valresp.respCode != "0") {
                return Json(new { status= true, respCode = "1", msg = valresp.respMsg }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, respCode = "0", msg = valresp.respMsg }, JsonRequestBehavior.AllowGet);
        }

        [Route("mlx/download/apk")]
        public FileResult downloadApk()
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(@"c:\MlExpressAPK\MLExpress_payout.apk");
            string fileName = "MLExpress_payout.apk";
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}