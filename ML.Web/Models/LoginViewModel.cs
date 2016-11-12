using System.ComponentModel.DataAnnotations;

namespace ML.Web.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string RedirectUrl { get; set; }
        
        public string dvid { get; set; }

        public string cellNum { get; set; }

        public string longt { get; set; }

        public string lat { get; set; }

        public string loc { get; set; }
        public string vrsn { get; set; }
    }
}