using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ML.Web.Models
{
    public class BillsPayViewModel
    {
        //Payers information
        public string payerID { get; set; }
        [Display(Name="Last name")]
        [Required]
        public string  lastname { get; set; }
        [Display(Name = "First name")]
        [Required]
        public string firstname { get; set; }
        [Display(Name = "Middle name")]
        public string middlename { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string  address { get; set; }
        [Display(Name = "Contact no")]
        [Required]
        public string contact { get; set; }

        //Account Information
        public string CompID { get; set; }
        [Display(Name="Company name")]
        [Required]
        public string CompName { get; set; }
        [Display(Name="Account no")]
        [Required]
        public string CompAccountNo { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string CompLname { get; set; }
        [Display(Name = "First name")]
        [Required]
        public string CompFname { get; set; }
        [Display(Name = "Middle Name")]
        public string CompMname { get; set; }

        //List of Company and accounts
        
        // Payment Information
        [Display(Name="Amount Paid")]
        [Required]
        public string amountPaid { get; set; }
        [Display(Name="Currency")]
        [Required]
        public string currency { get; set; }
        public IEnumerable<SelectListItem> scurrency
        {
            get
            {
                return new[]
                {

                    new SelectListItem {Value = "PHP",Text = "PHP" },
                };
            }
        }
        [Display(Name="Charge")]
        [Required]
        public string charge { get; set; }
        [Display(Name="Other Details")]
        [Required]
        public string otherDetails { get; set; }
    }

    public class CompanyList {
        public string CompanyName { get; set; }
        public string CompanyAccountNo { get; set; }
        public string CompanyLname { get; set; }
        public string CompanyFname { get; set; }
        public string CompanyMname { get; set; }
    }

    public class IEnumerableResult {
        public IEnumerable<ML.OFW.Contracts.Responses.billsPayResponse.CustomerSearchResponse> ListOfPayer { get; set; }
        public IEnumerable<ML.OFW.Contracts.Responses.billsPayResponse.partnersDetails> ListOfCompanies { get; set; }
        public IEnumerable<ML.OFW.Contracts.Responses.billsPayResponse.info> ListOfRecentTrans { get; set; }
    }

    public class ReceiptData {
        public string branch { get; set; }
        public string date { get; set; }
        public string paymentTo { get; set; }
        public string acctName { get; set; }
        public string Name { get; set; }
        public string address { get; set; }
        public string contactNo { get; set; }
        public string BPrefNo { get; set; }
        public string PaidAmt { get; set; }
        public string PaidCharge { get; set; }
        public string totalPaid { get; set; }
    }

    public class urlParam {
        public string kptn { get; set; }
    }
}