using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ML.Web.Models;
using System.IO;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using ML.Web.DataSet;
using System.ComponentModel;

namespace ML.Web.Controllers
{
    [RoutePrefix("mlx-billspay-report")]
    public class BillsPayReportController : Controller
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
        #region Transaction History Report
        [Route("trans-history")]
        [Authorize]
        public ActionResult HistoryReport()
        {
            var model = new ReportsViewModel();
            model.dtFrom = DateTime.Now;
            model.dtTo = DateTime.Now;
            return View(model);
        }

        [Route("trans-history")]
        [HttpPost]
        [Authorize]
        public JsonResult HistoryReport(string dtfrom, string dtto)
        {
            string[] values = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.MnthlyBalInqRptModel hReport = new OFW.Contracts.Models.MnthlyBalInqRptModel();
            hReport.AccountNo = values[1].Trim();
            hReport.dateFrom = Convert.ToDateTime(dtfrom);
            hReport.dateTo = Convert.ToDateTime(dtto);

            ML.OFW.Services.OFW ServiceH = new OFW.Services.OFW();
            var respVal = ServiceH.MnthlyBalInqRpt(hReport);

            if (respVal.RespCode != "0")
            {
                var model = new IenumerableReportResult();
                model.HistoryReport = respVal.BalInqDetails;
                int hasData = model.HistoryReport.Count();

                var model1 = new ReportsViewModel();
                model1.EndingBalance = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.EndingBalance));
                model1.TotalCredit = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.TotalCredit));
                model1.TotalDebit = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.TotalDebit));
                model1.PrintBy = respVal.PrintBy;
                model1.Date = respVal.DateNow;
                model1.runDate = respVal.RunDate;
                model1.TotalCreditCount = Convert.ToInt32(respVal.TotalNoCredit);
                model1.TotalDebitCount = Convert.ToInt32(respVal.TotalNoDebit);

                return Json(new
                {
                    status = true,
                    respCode = "1",
                    msg = respVal.RespMsg,
                    hasRecord = hasData,
                    otherData = model1,
                    historyData = RenderPartialViewToString("_reportHistory", model)
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { status = false, respCode = "0", msg = respVal.RespMsg }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Daily Report
        [Route("daily")]
        [Authorize]
        public ActionResult DailyReport()
        {
            var model = new ReportsViewModel();
            model.dtFrom = DateTime.Now;

            return View(model);
        }

        [Route("daily")]
        [HttpPost]
        [Authorize]
        public JsonResult DailyReport(string dtfrom)
        {
            string[] value = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.billsPayModel.DailyBillsPayReport smodel = new OFW.Contracts.Models.billsPayModel.DailyBillsPayReport();
            smodel.accountNo = value[1].Trim();
            smodel.UserName = value[0].Trim();
            smodel.date = Convert.ToDateTime(dtfrom);

            ML.OFW.Services.OFW service = new OFW.Services.OFW();

            var respVal = service.DailyBillspayReport(smodel);

            if (respVal.respCode != "0")
            {
                var model = new IenumerableReportResult();
                model.dailyReport = respVal.txnList;
                int hasData = model.dailyReport.Count();

                var model1 = new ReportsViewModel();

                model1.PrintBy = respVal.printBy;
                model1.Date = respVal.date;
                model1.runDate = respVal.runDate;
                model1.TotalCommission = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.totalCommission));
                model1.TotalAmount = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.totalAmountPHP));
                model1.dtFrom = Convert.ToDateTime(dtfrom);

                if (hasData > 0)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respMsg,
                        otherData = model1,
                        hasRecord = hasData,
                        dailyReport = RenderPartialViewToString("_reportDaily", model),
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    status = true,
                    respCode = "0",
                    msg = respVal.respMsg,
                    hasRecord = hasData
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = false,
                respCode = "0",
                msg = respVal.respMsg,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Monthly Report
        [Route("monthly")]
        [Authorize]
        public ActionResult MonthlyReport()
        {
            return View();
        }

        [Route("monthly")]
        [HttpPost]
        [Authorize]
        public JsonResult MonthlyReport(string year, string month)
        {

            string[] value = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.billsPayModel.MonthlyBillsPayReport smodel = new OFW.Contracts.Models.billsPayModel.MonthlyBillsPayReport();
            smodel.accountNo = value[1].Trim();
            smodel.UserName = value[0].Trim();
            smodel.month = Convert.ToInt32(month);
            smodel.year = Convert.ToInt32(year);

            ML.OFW.Services.OFW service = new OFW.Services.OFW();

            var respVal = service.MonthlySendoutBillsPayReport(smodel);

            var model = new IenumerableReportResult();
            if (respVal.respCode != "0")
            {
                model.monthlyReport = respVal.listTransactions;
                int hasData = model.monthlyReport.Count();

                var model1 = new ReportsViewModel();
                model1.PrintBy = respVal.printBy;
                model1.Date = respVal.date;
                model1.runDate = respVal.runDate;
                model1.TotalCount = respVal.totalCount.ToString();
                model1.TotalCommission = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.totalCommission));
                model1.TotalAmount = String.Format("{0:#,##0.00}", Convert.ToDecimal(respVal.totalPrincipal));

                if (hasData > 0)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respMsg,
                        transDate = respVal.runDate,
                        printBy = respVal.printBy,
                        Date = respVal.date,
                        hasRecord = hasData,
                        otherData = model1,
                        monthlyReport = RenderPartialViewToString("_reportMonthly", model),
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    status = true,
                    respCode = "0",
                    msg = respVal.respMsg,
                    hasRecord = hasData
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = false,
                respCode = "0",
                msg = respVal.respMsg,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Daily Cancellation Report
        [Route("daily-cancel-billspay")]
        [Authorize]
        public ActionResult DailyCancellationReport()
        {
            var model = new ReportsViewModel();
            model.dtFrom = DateTime.Now;

            return View(model);
        }

        [Route("daily-cancel-billspay")]
        [HttpPost]
        [Authorize]
        public JsonResult DailyCancellationReport(string dtfrom)
        {
            string[] value = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.billsPayModel.DailycancelBillspayReport smodel = new OFW.Contracts.Models.billsPayModel.DailycancelBillspayReport();
            smodel.accountNo = value[1].Trim();
            smodel.UserName = value[0].Trim();
            smodel.date = Convert.ToDateTime(dtfrom);

            ML.OFW.Services.OFW service = new OFW.Services.OFW();

            var respVal = service.DailycancelBillspayReport(smodel);

            var model = new IenumerableReportResult();
            if (respVal.respCode != "0")
            {
                model.dailyReport = respVal.txnList;
                int hasData = model.dailyReport.Count();

                if (hasData > 0)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respMsg,
                        transDate = respVal.runDate,
                        printBy = respVal.printBy,
                        Date = respVal.date,
                        hasRecord = hasData,
                        dailyReport = RenderPartialViewToString("_reportDaily", model),
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    status = true,
                    respCode = "0",
                    msg = respVal.respMsg,
                    hasRecord = hasData
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = false,
                respCode = "0",
                msg = respVal.respMsg,
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Monthly Cancellation Report
        [Route("monthly-cancel-billspay")]
        [Authorize]
        public ActionResult MonthlyCancellationReport()
        {
            return View();
        }

        [Route("monthly-cancel-billspay")]
        [HttpPost]
        [Authorize]
        public JsonResult MonthlyCancellationReport(string year, string month)
        {

            string[] value = User.Identity.Name.Split('|');

            ML.OFW.Contracts.Models.billsPayModel.MonthlyBillsPayReport smodel = new OFW.Contracts.Models.billsPayModel.MonthlyBillsPayReport();
            smodel.accountNo = value[1].Trim();
            smodel.UserName = value[0].Trim();
            smodel.month = Convert.ToInt32(month);
            smodel.year = Convert.ToInt32(year);

            ML.OFW.Services.OFW service = new OFW.Services.OFW();

            var respVal = service.MonthlyCancelBillsPayReport(smodel);

            var model = new IenumerableReportResult();
            if (respVal.respCode != "0")
            {
                model.monthlyReport = respVal.listTransactions;
                int hasData = model.monthlyReport.Count();

                if (hasData > 0)
                {
                    return Json(new
                    {
                        status = true,
                        respCode = "1",
                        msg = respVal.respMsg,
                        transDate = respVal.runDate,
                        printBy = respVal.printBy,
                        Date = respVal.date,
                        hasRecord = hasData,
                        monthlyReport = RenderPartialViewToString("_reportMonthly", model),
                    }, JsonRequestBehavior.AllowGet);
                }
                return Json(new
                {
                    status = true,
                    respCode = "0",
                    msg = respVal.respMsg,
                    hasRecord = hasData
                }, JsonRequestBehavior.AllowGet);
            }

            return Json(new
            {
                status = false,
                respCode = "0",
                msg = respVal.respMsg,
            }, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region Request For Change Report
        [Route("request-for-change")]
        [Authorize]
        public ActionResult RequestForChangeReport()
        {
            return View();
        }

        #endregion
        #region Initialize Reports and download report
        // History Report
        [Route("initialize-history-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializeHistoryReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');
            string[] paramValue = param.Split('|');
            ReportClass rptH = new ReportClass();
            dsTransHistory ds = new dsTransHistory();

            try
            {
                DataTable dt = ListToDataTable.ToDataTable(model.HistoryReport);

                rptH.FileName = Server.MapPath("~/CrystalReport/rptTransHistory.rpt");
                rptH.Load();

                ds.Tables[0].Merge(dt);
                rptH.SetDataSource(ds);

                rptH.SetParameterValue("@date", paramValue[0].Trim().ToUpper());
                rptH.SetParameterValue("@printBy", paramValue[2].Trim());
                rptH.SetParameterValue("@runDate", paramValue[1].Trim().ToUpper());
                rptH.SetParameterValue("@TotalCredit", Convert.ToDecimal(paramValue[6].Trim()));
                rptH.SetParameterValue("@TotalDebit", Convert.ToDecimal(paramValue[4].Trim()));
                rptH.SetParameterValue("@EndingBalance", Convert.ToDecimal(paramValue[7].Trim()));
                rptH.SetParameterValue("@CreditCount", Convert.ToInt32(paramValue[5].Trim()));
                rptH.SetParameterValue("@DebitCount", Convert.ToInt32(paramValue[3].Trim()));

            }
            catch (Exception ex)
            {
                return Json(new { status = false, resCode = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
            
          
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["HistorySession"] = stream;
            string pdfname = "history-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        // Daily Report
        [Route("initialize-daily-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializedailyReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');
            string[] paramValue = param.Split('|');
            ReportClass rptH = new ReportClass();
            dsDailyReport ds = new dsDailyReport();

            try
            {
                DataTable dt = ListToDataTable.ToDataTable(model.dailyReport);
                rptH.FileName = Server.MapPath("~/CrystalReport/rptDailyReport.rpt");
                rptH.Load();

                ds.Tables[0].Merge(dt);
                rptH.SetDataSource(ds);

                //rptH.SetParameterValue("@date", paramValue[0].Trim().ToUpper());
            }
            catch (Exception ex)
            {
                return Json(new { status = false, resCode = "0", msg = ex.Message }, JsonRequestBehavior.AllowGet);
            }

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["DailyRptSession"] = stream;
            string pdfname = "daily-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        //Monthly Report
        [Route("initialize-monthly-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializemonthlyReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');

            ReportClass rptH = new ReportClass();


            rptH.FileName = Server.MapPath("~/CrystalReport/rptDailyReport.rpt");

            rptH.Load();

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["MonthlyRptSession"] = stream;
            string pdfname = "daily-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        //Cancel Daily Report
        [Route("initialize-DailyCancel-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializeDailyCancelReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');

            ReportClass rptH = new ReportClass();


            rptH.FileName = Server.MapPath("~/CrystalReport/rptDailyReport.rpt");

            rptH.Load();

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["DailyCancelRptSession"] = stream;
            string pdfname = "daily-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        //Cancel Monthly Report
        [Route("initialize-MonthlyCancel-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializeMonthlyCancelReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');

            ReportClass rptH = new ReportClass();


            rptH.FileName = Server.MapPath("~/CrystalReport/rptDailyReport.rpt");

            rptH.Load();

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["MonthlyCancelRptSession"] = stream;
            string pdfname = "daily-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        //Daily RFC Report
        [Route("initialize-DailyRFC-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializeDailyRFCReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');

            ReportClass rptH = new ReportClass();


            rptH.FileName = Server.MapPath("~/CrystalReport/rptDailyReport.rpt");

            rptH.Load();

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["DailyRFCRptSession"] = stream;
            string pdfname = "daily-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        //Daily RFC Report
        [Route("initialize-MonthlyRFC-report")]
        [HttpPost]
        [Authorize]
        public JsonResult InitializeMonthlyRFCReport(IenumerableReportResult model, string param)
        {
            if (string.IsNullOrEmpty(param))
                return Json(new { status = false, resCode = "0", msg = "Something went wrong with report parameters. Please re-generate report or contact support. Thank you." }, JsonRequestBehavior.AllowGet);

            string[] values = User.Identity.Name.Split('|');

            ReportClass rptH = new ReportClass();


            rptH.FileName = Server.MapPath("~/CrystalReport/rptDailyReport.rpt");

            rptH.Load();

            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);

            rptH.Close();
            rptH.Dispose();
            rptH = null;

            Session["MonthlyRFCRptSession"] = stream;
            string pdfname = "daily-report" + DateTime.Now;

            return Json(new { status = true, resCode = "1", msg = "Success", pdfName = pdfname }, JsonRequestBehavior.AllowGet);
        }
        // Download PDF
        [Route("download-file")]
        [HttpGet]
        [Authorize]
        public ActionResult Download(int type, string pdfname)
        {
            string sessionName = string.Empty;

            switch (type)
            {
                case 0:
                    sessionName = "HistorySession";
                    break;
                case 1:
                    sessionName = "DailyRptSession";
                    break;
                case 2:
                    sessionName = "MonthlyRptSession";
                    break;
                case 3:
                    sessionName = "DailyCancelRptSession";
                    break;
                case 4:
                    sessionName = "MonthlyCancelRptSession";
                    break;
                case 5:
                    sessionName = "DailyRFCRptSession";
                    break;
                case 6:
                    sessionName = "MonthlyRFCRptSession";
                    break;
                default:
                    sessionName = null;
                    break;
            }
            var ms = Session[sessionName] as MemoryStream;

            Session[sessionName] = null;

            if (ms == null)
                return new EmptyResult();

            return File(ms, "application/pdf", pdfname + ".pdf");
            //return File("<your path>" + "", System.Net.Mime.MediaTypeNames.Application.Octet);
        }
        #endregion
    }
    #region Convert List to DataTable
    // Convert List to DataTable Static Class
    public static class ListToDataTable
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name, property.PropertyType);
            }
            object[] values = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
    }
    #endregion
}