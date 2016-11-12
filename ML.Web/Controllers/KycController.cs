using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ML.Web.Models;

namespace ML.Web.Controllers
{
    [RoutePrefix("mlx-billspay-kyc")]
    public class KycController : Controller
    {
        [Route("add")]
        [Authorize]
        public ActionResult AddKyc(KycViewModel model,string q)
        {
            if (string.IsNullOrEmpty(q)) {
                model.bdate = DateTime.Now;
                model.ExpiryDate = DateTime.Now;
                model.isUpdate = 0;
            } else {

                ML.OFW.Contracts.Models.billsPayModel.searchpayorbyId smodel = new OFW.Contracts.Models.billsPayModel.searchpayorbyId();
                smodel.custId = q.Trim();
                smodel.page = 1;
                smodel.perPage = 5;

                ML.OFW.Services.OFW service = new OFW.Services.OFW();
                var respVal = service.searchpayorbyId(smodel);

                if (respVal.respCode != 0) {

                    model.bdate = Convert.ToDateTime(respVal.birthDate);
                    model.bplace = respVal.placeOfBirth;
                    model.currentAdd = respVal.currentAdd;
                    model.custID = respVal.custId;
                    model.email = respVal.emailAdd;
                    model.ExpiryDate = Convert.ToDateTime(respVal.expiryDate);
                    model.Fname = respVal.firstName;
                    model.gender = respVal.gender;
                    model.Idno = respVal.IDNo;
                    model.selectcountry = respVal.IDType;
                    model.isUpdate = 1;
                    model.Lname = respVal.lastName;
                    model.Mname = respVal.middleName;
                    model.mobileNo = respVal.mobileNo;
                    model.nationality = respVal.nationality;
                    model.permanentAdd = respVal.permanentAdd;
                    model.phoneNo = respVal.phoneNo;
                    model.provState = respVal.provinceState;
                    model.selectcountry = respVal.country;
                }
                else
                {
                    return Json(new { status = false, respCode = "0", msg = respVal.errorDetail });
                }
            }

            return View(model);
        }

        //[Route("select-kyc")]
        //[HttpPost]
        //[Authorize]
        //public JsonResult kycInfo(string userId)
        //{
        //    return Json("");
        //}

        [Route("submit-kyc")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult Submitkyc(KycViewModel model)
        {
            string[] value = User.Identity.Name.Split('|');

            if (ModelState.IsValid)
            {
                ML.OFW.Contracts.Models.billsPayModel.payorAddKYC smodel = new OFW.Contracts.Models.billsPayModel.payorAddKYC();
                smodel.birthDate = model.bdate.ToString();
                smodel.country = model.selectcountry;
                smodel.createdBy = value[0];
                smodel.currAdd = model.currentAdd;
                smodel.emailAdd = model.email;
                smodel.expiryDate = model.ExpiryDate.ToString();
                smodel.fName = model.Fname;
                smodel.gender = model.gender;
                smodel.idNo = model.Idno;
                smodel.idType = model.SelectedIDtype;
                smodel.lName = model.Lname;
                smodel.mName = model.Mname;
                smodel.mobileNo = model.mobileNo;
                smodel.nationality = model.nationality;
                smodel.permanentAdd = model.permanentAdd;
                smodel.phoneNo = model.phoneNo;
                smodel.placeOfBirth = model.bplace;
                smodel.province = model.provState;

                ML.OFW.Services.OFW service = new OFW.Services.OFW();
                var respVal = service.payorAddKYC(smodel);

                if (respVal.respCode != 0)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respMsg,
                        PayorData = new
                        {
                            payorID = respVal.custId,
                            payorLname = model.Lname,
                            payorFname = model.Fname,
                            payorMname = model.Mname,
                            payorContact = string.IsNullOrEmpty(model.mobileNo) ? model.phoneNo : model.mobileNo,
                            payorAddress = model.permanentAdd
                        }
                    });
                }
                return Json(new { status = false, respCode = "0", msg = respVal.errorDetail });


            }
            return Json(new { status = false, respCode = "0", msg = "Please verify your data." });
        }

        [Route("update-kyc")]
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateKyc(KycViewModel model) {
            string[] value = User.Identity.Name.Split('|');

            if (ModelState.IsValid) {
                if (model.custID == "" | model.custID == null) {
                    return Json(new { status = false, respCode = "0", msg = "Please verify your data." });
                }

                ML.OFW.Contracts.Models.billsPayModel.payorUpdateKYC smodel = new OFW.Contracts.Models.billsPayModel.payorUpdateKYC();
                smodel.birthDate = model.bdate.ToString();
                smodel.country = model.selectcountry;
                smodel.currAdd = model.currentAdd;
                smodel.emailAdd = model.email;
                smodel.expiryDate = model.ExpiryDate.ToString();
                smodel.fName = model.Fname;
                smodel.gender = model.gender;
                smodel.custId = model.custID;
                smodel.modifiedBy = value[0];
                smodel.IDNo = model.Idno;
                smodel.IDType = model.SelectedIDtype;
                smodel.lName = model.Lname;
                smodel.mName = model.Mname;
                smodel.mobileNo = model.mobileNo;
                smodel.nationality = model.nationality;
                smodel.permanentAdd = model.permanentAdd;
                smodel.phoneNo = model.phoneNo;
                smodel.placeOfBirth = model.bplace;
                smodel.province = model.provState;

                ML.OFW.Services.OFW service = new OFW.Services.OFW();
                var respVal = service.payorUpdateKYC(smodel);

                if (respVal.respCode != 0)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respMsg,
                        PayorData = new
                        {
                            payorID = respVal.custId,
                            payorLname = model.Lname,
                            payorFname = model.Fname,
                            payorMname = model.Mname,
                            payorContact = string.IsNullOrEmpty(model.mobileNo) ? model.phoneNo : model.mobileNo,
                            payorAddress = model.permanentAdd
                        }
                    });
                }
                return Json(new { status = false, respCode = "0", msg = respVal.errorDetail });
            }
            return Json(new { status = false, respCode = "0", msg = "Please verify your data." });
        }
    }
}