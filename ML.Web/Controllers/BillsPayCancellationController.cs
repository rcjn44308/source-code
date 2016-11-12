using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ML.Web.Models;

namespace ML.Web.Controllers
{
    [RoutePrefix("cancellation")]
    public class BillsPayCancellationController : Controller
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

        [Route("billspay-sendout")]
        [Authorize]
        public ActionResult CancelBillsPayForm()
        {
            return View();
        }

        [Route("billspay-sendout")]
        [HttpPost]
        [Authorize]
        public JsonResult CancelBillsPayForm(string kptnOrRef, string details)
        {
            string[] value = User.Identity.Name.Split('|');

            ML.OFW.Services.OFW service1 = new OFW.Services.OFW();
            var model2 = service1.getChargeName();

            ML.OFW.Contracts.Models.billsPayModel.cancelbillspayment smodel = new OFW.Contracts.Models.billsPayModel.cancelbillspayment();
            smodel.cancelDetails = details;
            smodel.kptn = kptnOrRef.Trim();
            smodel.walletuser = value[0].Trim();
            smodel.walletno = value[1].Trim();
            smodel.longitude = value[3].Trim();
            smodel.latitute = value[4].Trim();
            smodel.location = value[5].Trim();
            smodel.deviceID = value[6].Trim();

            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.cancelBillspayment(smodel);

            if (respVal.respCode != "0")
            {
                return Json(new { status = true, respCode = "1", msg = respVal.respMsg, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, respCode = "0", msg = respVal.respMsg, }, JsonRequestBehavior.AllowGet);
        }

        [Route("billspay-rfc")]
        [Authorize]
        public ActionResult RfcBillsPaymentForm()
        {
            return View();
        }

        [Route("billspay-rfc")]
        [HttpPost]
        [Authorize]
        public JsonResult RfcBillsPaymentForm(CancelBillsPayViewModel model)
        {
            string[] value = User.Identity.Name.Split('|');

            ML.OFW.Services.OFW service1 = new OFW.Services.OFW();
            var model2 = service1.getChargeName();

            ML.OFW.Contracts.Models.billsPayModel.changeDetailsBillspayment smodel = new OFW.Contracts.Models.billsPayModel.changeDetailsBillspayment();
            smodel.accountFirstName = model.compFname;
            smodel.accountLastName = model.compLname;
            smodel.accountMiddleName = model.compMname;
            smodel.accountNo = model.Accountno;
            smodel.cancelDetails = model.reason;
            smodel.custID = "";
            smodel.kptn = "";
            smodel.walletVersion = "";
            smodel.walletuser = value[0].Trim();
            smodel.walletno = value[1].Trim();
            smodel.longitude = value[3].Trim();
            smodel.latitute = value[4].Trim();
            smodel.location = value[5].Trim();
            smodel.deviceID = value[6].Trim();

            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.changeDetailsBillspayment(smodel);

            if (respVal.respCode != "0")
            {
                return Json(new { status = true, respCode = "1", msg = respVal.respMsg, }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { status = false, respCode = "0", msg = respVal.respMsg, }, JsonRequestBehavior.AllowGet);
        }


        [Route("search-billspay-kptn")]
        [HttpPost]
        [Authorize]
        public JsonResult kptnDetails(string kptnOrRef, string CompID)
        {

            string[] value = User.Identity.Name.Split('|');
            var model = new CancelBillsPayViewModel();

            ML.OFW.Contracts.Models.billsPayModel.searchBPX_KPTN smodel = new OFW.Contracts.Models.billsPayModel.searchBPX_KPTN();
            smodel.kptn = kptnOrRef.Trim();
            smodel.walletuser = value[0].Trim();
            smodel.companyID = CompID.Trim();

            ML.OFW.Services.OFW service = new OFW.Services.OFW();
            var respVal = service.searchBPX_KPTN(smodel);

            if (respVal.respCode != "0")
            {
                model.Accountno = respVal.accountNo;
                model.Address = respVal.payorAddress;
                model.AmountPaid = string.Format("{0:#,##0.00}", respVal.totalAmountPaid);
                model.CancellationCharge = string.Format("{0:#,##0.00}", respVal.customerCharge);
                model.compFname = respVal.accountFirstName;
                model.compLname = respVal.accountLastName;
                model.compMname = respVal.accountMiddleName;
                model.compName = respVal.partnersAccountName;
                model.contactNo = respVal.payorContact;
                model.ControlNo = respVal.controlNo;
                model.date = respVal.sendoutDate.ToString();
                model.Firstname = respVal.payorFirstName;
                model.Lastname = respVal.payorLastName;
                model.Middlename = respVal.payorMiddleName;
                model.Operator = value[0].Trim();
                model.OtherDetails = "";

                return Json(new { status = true, respCode = "1", msg = respVal.resMsg, modelDetails = RenderPartialViewToString("_cancellationDetails", model) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, respCode = "0", msg = respVal.resMsg }, JsonRequestBehavior.AllowGet);
        }

    }
}