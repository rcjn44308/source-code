using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ML.Web.Models
{
    public class CancelBillsPayViewModel
    {
        public string date { get; set; }
        public string ControlNo { get; set; }
        public string Operator { get; set; }

        public string compName { get; set; }
        public string Accountno { get; set; }
        public string compLname { get; set; }
        public string compFname { get; set; }
        public string compMname { get; set; }

        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Address { get; set; }
        public string contactNo { get; set; }

        public string AmountPaid { get; set; }
        public string CancellationCharge { get; set; }
        public string OtherDetails { get; set; }
        public string reason { get; set; }

    }
}