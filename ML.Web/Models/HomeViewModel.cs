using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ML.Web.Models
{
    public class HomeViewModel
    {
        public string username { get; set; }
        public string location { get; set; }
        public string longhitude { get; set; }
        public string latitude { get; set; }
        public string Address { get; set; }
        public string contactNo { get; set; }
        public string emailAdd { get; set; }
        public string fullname { get; set; }
    }
}