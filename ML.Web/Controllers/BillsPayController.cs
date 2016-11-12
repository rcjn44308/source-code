using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ML.Web.Models;
using System.IO;

namespace ML.Web.Controllers
{
    [RoutePrefix("mlx-bills-pay")]
    public class BillsPayController : Controller
    {
        //Convert Partial view to string
        protected string RenderPartialViewToString()
        {
            return RenderPartialViewToString(null, null);
        }

        protected string RenderPartialViewToString(string viewName)
        {
            return RenderPartialViewToString(viewName, null);
        }

        protected string RenderPartialViewToString(object model)
        {
            return RenderPartialViewToString(null, model);
        }

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }
        // End
        public bool checkSession()
        {
            bool status = false;
            string _encryptedTicket = string.Empty;
            System.Web.HttpContext curContext = System.Web.HttpContext.Current;

            HttpCookie _cookie = curContext.Request.Cookies["2ndsample"];

            if (!string.IsNullOrEmpty(_cookie.Value))
            {
                _encryptedTicket = _cookie.Value;
                FormsAuthenticationTicket _ticket = FormsAuthentication.Decrypt(_encryptedTicket);

                if (!_ticket.Expired)
                {
                    IIdentity _identity = new FormsIdentity(_ticket);
                    IPrincipal _principal = new GenericPrincipal(_identity, new string[0]); //Identity plus string of roles.        
                    status = true;
                }
            }

            return status;
        }

        [Route("billspay-form")]
        [Authorize]
        public ActionResult BillsPayForm()
        {
            bool sss  = checkSession();

            var model = new BillsPayViewModel();
            return View(model);
        }

        [Route("search-payer")]
        [HttpPost]
        [Authorize]
        public JsonResult searchpayer(string Fname, string Lname, string Mname)
        {

            ML.OFW.Contracts.Models.billsPayModel.payorSearch smodel = new OFW.Contracts.Models.billsPayModel.payorSearch();

            smodel.firstName = Fname.Trim();
            smodel.lastName = Lname.Trim();
            smodel.middleName = Mname;
            smodel.page = 1;
            smodel.perPage = 100;

            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.payorSearch(smodel);

            var model = new IEnumerableResult();
            model.ListOfPayer = respVal.data;

            if (respVal.respCode != 0)
            {
                if (model.ListOfPayer != null)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.message,
                        ListOfPayer = RenderPartialViewToString("_resultSearchPayer", model)
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        respCode = "0",
                        msg = respVal.message
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { status = false, respCode = "0", msg = respVal.message }, JsonRequestBehavior.AllowGet);
        }

        [Route("recent-transaction")]
        [HttpPost]
        [Authorize]
        public JsonResult ListOfrecentTrans(string custID)
        {
            ML.OFW.Contracts.Models.billsPayModel.recentTransaction smodel = new OFW.Contracts.Models.billsPayModel.recentTransaction();
            smodel.customersID = custID.Trim();

            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.recentTransaction(smodel);

            var model = new IEnumerableResult();
            model.ListOfRecentTrans = respVal.recentListTransaction;

            if (respVal.respcode != "0")
            {
                if (model.ListOfRecentTrans != null)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respmsg,
                        ListOfRecentTrans = RenderPartialViewToString("_resultRecentTrans", model)
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        status = false,
                        respCode = "0",
                        msg = respVal.respmsg
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new
            {
                status = false,
                respCode = "0",
                msg = respVal.respmsg
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("company-list")]
        [HttpGet]
        [Authorize]
        public JsonResult ListOfCompany()
        {
            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.partnerslist();

            var model = new IEnumerableResult();
            model.ListOfCompanies = respVal.partnersInfo;

            if (respVal.respCode != "0")
            {
                if (respVal.partnersInfo != null)
                {
                    return Json(new
                    {
                        status = true,
                        errorcode = "1",
                        msg = respVal.respMsg,
                        ListofCompany = RenderPartialViewToString("_resultViewCompanies", model)
                    }, JsonRequestBehavior.AllowGet);
                }
                else {
                    return Json(new
                    {
                        status = false,
                        respCode = "0",
                        msg = respVal.respMsg
                    }, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { status = false, errorcode = "0", errormsg = respVal.respMsg });
        }

        [Route("calculate-charge")]
        [HttpPost]
        [Authorize]
        public JsonResult CalculateCharge(string acctID, string Curr)
        {
            ML.OFW.Contracts.Models.billsPayModel.partnerschargebills smodel = new OFW.Contracts.Models.billsPayModel.partnerschargebills();
            smodel.Accountid = acctID;
            smodel.currency = Curr;

            ML.OFW.Services.OFW service = new OFW.Services.OFW();

            var respval = service.partnerschargebills(smodel);
            if (respval.respcode != 0) {
                return Json(new
                {
                    status = true,
                    errorcode = "1",
                    msg = respval.message,
                    charge = respval.calc.chargeamt
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false,
                respCode = "0",
                msg = respval.message
            }, JsonRequestBehavior.AllowGet);
        }

        [Route("submit-billspay")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult SubmitBillsPay(BillsPayViewModel model)
        {

            if (ModelState.IsValid)
            {
                string[] values = User.Identity.Name.Split('|');

                ML.OFW.Contracts.Models.billsPayModel.saveTransactionBillsPay smodel = new OFW.Contracts.Models.billsPayModel.saveTransactionBillsPay();

                smodel.accountFName = model.CompFname;
                smodel.accountLName = model.CompLname;
                smodel.accountMName = model.CompMname;
                smodel.accountNo = model.CompAccountNo;
                smodel.amountpaid = Convert.ToDecimal(model.amountPaid);
                smodel.companyID = model.CompID;
                smodel.currency = model.currency;
                smodel.customerCharge = Convert.ToDecimal(model.charge);
                smodel.otherDetails = model.otherDetails;
                smodel.payorCustID = model.payerID;
                smodel.walletno = values[1];
                smodel.walletOperator = values[0];

                ML.OFW.Services.OFW service = new OFW.Services.OFW();
                var respVal = service.saveTransactionBillsPay(smodel);

                if (respVal.respcode != 0)
                {
                    return Json(new { status = true, errorcode = "1", msg = respVal.message, kptn = respVal.kptn }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, errorcode = "0", msg = respVal.message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { status = false, errorcode = "0", msg = "Form is invalid." }, JsonRequestBehavior.AllowGet);
            }
        }

        [Route("view-receipt")]
        [HttpGet]
        [Authorize]
        public ActionResult ViewReceipt(string kptn) {
            var model = new urlParam();
            model.kptn = kptn;

            return View(model);
        }

        [Route("get-receipt-data")]
        [Authorize]
        public JsonResult GetReceiptData(string kptn) {

            if(string.IsNullOrEmpty(kptn))
                return Json(new { status = false, errorcode = "0", msg = "Something went wrong with your parameter" }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.billsPayModel.reprintingpartners smodel = new OFW.Contracts.Models.billsPayModel.reprintingpartners();
            smodel.kptnno = kptn.Trim();
            smodel.walletuser = values[0].Trim();
            smodel.walletno = values[1].Trim();

            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.reprintBillspay(smodel);

            if (respVal.respCode != "0") {

                var model = new ReceiptData();

                model.acctName = respVal.accountName;
                model.address = respVal.address;
                model.BPrefNo = respVal.refNo;
                model.branch = respVal.branchName;
                model.contactNo = respVal.contactNo;
                model.date = respVal.transDate;
                model.Name = respVal.name;
                model.PaidAmt = string.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.principalAmount));
                model.PaidCharge = string.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.charge));
                model.paymentTo = respVal.paymentTo;
                model.totalPaid = string.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.totalAmountPaid));


                return Json(new { status = true, errorcode = "1", msg = respVal.respMsg, receiptData = model }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, errorcode = "0", msg = respVal.respMsg }, JsonRequestBehavior.AllowGet);
        }

        [Route("re-print")]
        [Authorize]
        public ActionResult Reprint()
        {
            return View();
        }
    }
}