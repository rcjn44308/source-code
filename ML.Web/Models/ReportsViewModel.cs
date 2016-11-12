using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ML.Web.Models
{
    public class ReportsViewModel
    {
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dtFrom { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dtTo { get; set; }

        public string TotalCommission { get; set; }
        public string TotalAmount { get; set; }
        public string TotalCharge { get; set; }
        public string TotalCount { get; set; }
        public string PrintBy { get; set; }
        public string runDate { get; set; }
        public string Date { get; set; }
        public string EndingBalance { get; set; }
        public string TotalCredit { get; set; }
        public string TotalDebit { get; set; }
        public int TotalCreditCount { get; set; }
        public int TotalDebitCount { get; set; }
    }

    public class IenumerableReportResult
    {
        public List<ML.OFW.Contracts.Responses.billsPayResponse.billsPayTransaction> dailyReport { get; set; }
        public List<ML.OFW.Contracts.Responses.billsPayResponse.DailyBillsPay> monthlyReport { get; set; }
        public List<ML.OFW.Contracts.Responses.BalInqDetails> HistoryReport { get; set; }
    }
}