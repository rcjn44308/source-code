﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ML.Web.Models
{
    public class KycViewModel
    {
        [Display(Name="First name")]
        [Required]
        public string Fname { get; set; }
        [Display(Name = "Middle name")]
        [Required]
        public string Mname { get; set; }
        [Display(Name = "Last name")]
        [Required]
        public string Lname { get; set; }
        [Display(Name = "Birth date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? bdate { get; set; }
        [Display(Name = "Birth place")]
        [Required]
        public string bplace { get; set; }
        [Display(Name = "Gender")]
        [Required]
        public string gender { get; set; }
        public IEnumerable<SelectListItem> sgender
        {
            get
            {
                return new[]
                {
                    new SelectListItem {Value = "MALE",Text = "MALE" },
                    new SelectListItem {Value = "FEMALE",Text = "FEMALE" },
                };
            }
        }
        [Display(Name = "Permanent address")]
        [Required]
        public string permanentAdd { get; set; }
        [Display(Name = "Current address")]
        [Required]
        public string currentAdd { get; set; }
        [Display(Name = "Province / State")]
        [Required]
        public string provState { get; set; }
        [Display(Name = "Country")]
        [Required]
        public string selectcountry { get; set; }
        public IEnumerable<SelectListItem> listcountry
        {
            get
            {
                return new[]
                {
                    new SelectListItem {Value = "Afghanistan", Text = "Afghanistan"},
                    new SelectListItem {Value = "Albania", Text = "Albania"},
                    new SelectListItem {Value = "Algeria", Text = "Algeria"},
                    new SelectListItem {Value = "America Samoa", Text = "America Samoa"},
                    new SelectListItem {Value = "Andorra", Text = "Andorra"},
                    new SelectListItem {Value = "Angola", Text = "Angola"},
                    new SelectListItem {Value = "Anguilla", Text = "Anguilla"},
                    new SelectListItem {Value = "Antarctica", Text = "Antarctica"},
                    new SelectListItem {Value = "Antigua and Barbuda", Text = "Antigua and Barbuda"},
                    new SelectListItem {Value = "Argentina", Text = "Argentina"},
                    new SelectListItem {Value = "Armenia", Text = "Armenia"},
                    new SelectListItem {Value = "Aruba", Text = "Aruba"},
                    new SelectListItem {Value = "Ascension", Text = "Ascension"},
                    new SelectListItem {Value = "Australia", Text = "Australia"},
                    new SelectListItem {Value = "Austria", Text = "Austria"},
                    new SelectListItem {Value = "Azerbaijan", Text = "Azerbaijan"},

                    new SelectListItem {Value = "Bahamas", Text = "Bahamas"},
                    new SelectListItem {Value = "Bahrain", Text = "Bahrain"},
                    new SelectListItem {Value = "Bangladesh", Text = "Bangladesh"},
                    new SelectListItem {Value = "Barbados", Text = "Barbados"},
                    new SelectListItem {Value = "Belarus", Text = "Belarus"},
                    new SelectListItem {Value = "Belgium", Text = "Belgium"},
                    new SelectListItem {Value = "Belize", Text = "Belize"},
                    new SelectListItem {Value = "Benin", Text = "Benin"},
                    new SelectListItem {Value = "Bermuda", Text = "Bermuda"},
                    new SelectListItem {Value = "Bhutan", Text = "Bhutan"},
                    new SelectListItem {Value = "Bolivia", Text = "Bolivia"},
                    new SelectListItem {Value = "Bosnia and Herzegovina", Text = "Bosnia and Herzegovina"},
                    new SelectListItem {Value = "Botswana", Text = "Botswana"},
                    new SelectListItem {Value = "Bouvet Island", Text = "Bouvet Island"},
                    new SelectListItem {Value = "Brazil", Text = "Brazil"},
                    new SelectListItem {Value = "British Indian Ocean Territory", Text = "British Indian Ocean Territory"},
                    new SelectListItem {Value = "Brunei Darussalam", Text = "Brunei Darussalam"},
                    new SelectListItem {Value = "Bulgaria", Text = "Bulgaria"},
                    new SelectListItem {Value = "Burkina Faso", Text = "Burkina Faso"},
                    new SelectListItem {Value = "Burundi", Text = "Burundi"},

                    new SelectListItem {Value = "Cambodia", Text = "Cambodia"},
                    new SelectListItem {Value = "Cameroon", Text = "Cameroon"},
                    new SelectListItem {Value = "Canada", Text = "Canada"},
                    new SelectListItem {Value = "Cafe Verde", Text = "Cafe Verde"},
                    new SelectListItem {Value = "Cayman Islands", Text = "Cayman Islands"},
                    new SelectListItem {Value = "Central African Republic", Text = "Central African Republic"},
                    new SelectListItem {Value = "Chad", Text = "Chad"},
                    new SelectListItem {Value = "Chile", Text = "Chile"},
                    new SelectListItem {Value = "China", Text = "China"},
                    new SelectListItem {Value = "Christmas Island", Text = "Christmas Island"},
                    new SelectListItem {Value = "Cocos (keeling) Island", Text = "Cocos (keeling) Island"},
                    new SelectListItem {Value = "Colombia", Text = "Colombia"},
                    new SelectListItem {Value = "Comoros", Text = "Comoros"},
                    new SelectListItem {Value = "Congo (DRC)(Kinshasa)", Text = "Congo (DRC)(Kinshasa)"},
                    new SelectListItem {Value = "Congo (Republic)(Congo-Brazzaville)", Text = "Congo (Republic)(Congo-Brazzaville)"},
                    new SelectListItem {Value = "Cook Islands", Text = "Cook Islands"},
                    new SelectListItem {Value = "Costa Rica", Text = "Costa Rica"},
                    new SelectListItem {Value = "Côte d'Ivoire", Text = "Côte d'Ivoire"},
                    new SelectListItem {Value = "Croatia", Text = "Croatia"},
                    new SelectListItem {Value = "Cuba", Text = "Cuba"},
                    new SelectListItem {Value = "Curaçao", Text = "Curaçao"},
                    new SelectListItem {Value = "Cyprus", Text = "Cyprus"},
                    new SelectListItem {Value = "Czech Republic", Text = "Czech Republic"},

                    new SelectListItem {Value = "Denmark", Text = "Denmark"},
                    new SelectListItem {Value = "Djibouti", Text = "Djibouti"},
                    new SelectListItem {Value = "Dominica", Text = "Dominica"},
                    new SelectListItem {Value = "Dominican Republic", Text = "Dominican Republic"},

                    new SelectListItem {Value = "East Timor", Text = "East Timor"},
                    new SelectListItem {Value = "Ecuador", Text = "Ecuador"},
                    new SelectListItem {Value = "Egypt", Text = "Egypt"},
                    new SelectListItem {Value = "El Salvador", Text = "El Salvador"},
                    new SelectListItem {Value = "Equatorial Guinea", Text = "Equatorial Guinea"},
                    new SelectListItem {Value = "Eritrea", Text = "Eritrea"},
                    new SelectListItem {Value = "Estonia", Text = "Estonia"},
                    new SelectListItem {Value = "Ethiopia", Text = "Ethiopia"},

                    new SelectListItem {Value = "Falkland Islands", Text = "Falkland Islands"},
                    new SelectListItem {Value = "Faroe Islands", Text = "Faroe Islands"},
                    new SelectListItem {Value = "Fiji", Text = "Fiji"},
                    new SelectListItem {Value = "Finland", Text = "Finland"},
                    new SelectListItem {Value = "France", Text = "France"},
                    new SelectListItem {Value = "French Guiana", Text = "French Guiana"},
                    new SelectListItem {Value = "French Polynesia", Text = "French Polynesia"},

                    new SelectListItem {Value = "Gabon", Text = "Gabon"},
                    new SelectListItem {Value = "Gambia", Text = "Gambia"},
                    new SelectListItem {Value = "Georgia", Text = "Georgia"},
                    new SelectListItem {Value = "Germany", Text = "Germany"},
                    new SelectListItem {Value = "Ghana", Text = "Ghana"},
                    new SelectListItem {Value = "Gibraltar", Text = "Gibraltar"},
                    new SelectListItem {Value = "Greece", Text = "Greece"},
                    new SelectListItem {Value = "Greenland", Text = "Greenland"},
                    new SelectListItem {Value = "Grenada", Text = "Grenada"},
                    new SelectListItem {Value = "Guadeloupe", Text = "Guadeloupe"},
                    new SelectListItem {Value = "Guam", Text = "Guam"},
                    new SelectListItem {Value = "Guatemala", Text = "Guatemala"},
                    new SelectListItem {Value = "Guernsey", Text = "Guernsey"},
                    new SelectListItem {Value = "Guinea", Text = "Guinea"},
                    new SelectListItem {Value = "Guinea-Bissau", Text = "Guinea-Bissau"},
                    new SelectListItem {Value = "Guyana", Text = "Guyana"},

                    new SelectListItem {Value = "Haiti", Text = "Haiti"},
                    new SelectListItem {Value = "Honduras", Text = "Honduras"},
                    new SelectListItem {Value = "Hong Kong", Text = "Hong Kong"},
                    new SelectListItem {Value = "Hungary", Text = "Hungary"},

                    new SelectListItem {Value = "Iceland", Text = "Iceland"},
                    new SelectListItem {Value = "India", Text = "India"},
                    new SelectListItem {Value = "Indonesia", Text = "Indonesia"},
                    new SelectListItem {Value = "Iran", Text = "Iran"},
                    new SelectListItem {Value = "Iraq", Text = "Iraq"},
                    new SelectListItem {Value = "Ireland", Text = "Ireland"},
                    new SelectListItem {Value = "Isle of Man", Text = "Isle of Man"},
                    new SelectListItem {Value = "Israel", Text = "Israel"},
                    new SelectListItem {Value = "Italy", Text = "Italy"},

                    new SelectListItem {Value = "Jamaica", Text = "Jamaica"},
                    new SelectListItem {Value = "Japan", Text = "Japan"},
                    new SelectListItem {Value = "Jersey", Text = "Jersey"},
                    new SelectListItem {Value = "Jordan", Text = "Jordan"},

                    new SelectListItem {Value = "Kazakhstan", Text = "Kazakhstan"},
                    new SelectListItem {Value = "Kenya", Text = "Kenya"},
                    new SelectListItem {Value = "Kiribati", Text = "Kiribati"},
                    new SelectListItem {Value = "Kosovo", Text = "Kosovo"},
                    new SelectListItem {Value = "Kuwait", Text = "Kuwait"},
                    new SelectListItem {Value = "Kyrgyzstan", Text = "Kyrgyzstan"},

                    new SelectListItem {Value = "Laos", Text = "Laos"},
                    new SelectListItem {Value = "Latvia", Text = "Latvia"},
                    new SelectListItem {Value = "Lebanon", Text = "Lebanon"},
                    new SelectListItem {Value = "Lesotho", Text = "Lesotho"},
                    new SelectListItem {Value = "Liberia", Text = "Liberia"},
                    new SelectListItem {Value = "Libya", Text = "Libya"},
                    new SelectListItem {Value = "Liechtenstein", Text = "Liechtenstein"},
                    new SelectListItem {Value = "Lithuania", Text = "Lithuania"},
                    new SelectListItem {Value = "Luxembourg", Text = "Luxembourg"},

                    new SelectListItem {Value = "Macedonia", Text = "Macedonia"},
                    new SelectListItem {Value = "Madagascar", Text = "Madagascar"},
                    new SelectListItem {Value = "Malawi", Text = "Malawi"},
                    new SelectListItem {Value = "Malaysia", Text = "Malaysia"},
                    new SelectListItem {Value = "Maldives", Text = "Maldives"},
                    new SelectListItem {Value = "Mali", Text = "Mali"},
                    new SelectListItem {Value = "Malta", Text = "Malta"},
                    new SelectListItem {Value = "Marshall Islands", Text = "Marshall Islands"},
                    new SelectListItem {Value = "Martinique", Text = "Martinique"},
                    new SelectListItem {Value = "Mauritania", Text = "Mauritania"},
                    new SelectListItem {Value = "Mauritius", Text = "Mauritius"},
                    new SelectListItem {Value = "Mayotte", Text = "Mayotte"},
                    new SelectListItem {Value = "Mexico", Text = "Mexico"},
                    new SelectListItem {Value = "Federated States of Micronesia", Text = "Federated States of Micronesia"},
                    new SelectListItem {Value = "Moldova", Text = "Moldova"},
                    new SelectListItem {Value = "Monaco", Text = "Monaco"},
                    new SelectListItem {Value = "Mongolia", Text = "Mongolia"},
                    new SelectListItem {Value = "Montenegro", Text = "Montenegro"},
                    new SelectListItem {Value = "Montserrat", Text = "Montserrat"},
                    new SelectListItem {Value = "Morocco", Text = "Morocco"},
                    new SelectListItem {Value = "Mozambique", Text = "Mozambique"},

                    new SelectListItem {Value = "Nagorno-Karabakh", Text = "Nagorno-Karabakh"},
                    new SelectListItem {Value = "Namibia", Text = "Namibia"},
                    new SelectListItem {Value = "Nauru", Text = "Nauru"},
                    new SelectListItem {Value = "Nepal", Text = "Nepal"},
                    new SelectListItem {Value = "Netherlands", Text = "Netherlands"},
                    new SelectListItem {Value = "New Caledonia", Text = "New Caledonia"},
                    new SelectListItem {Value = "New Zealand", Text = "New Zealand"},
                    new SelectListItem {Value = "Nicaragua", Text = "Nicaragua"},
                    new SelectListItem {Value = "Niger", Text = "Niger"},
                    new SelectListItem {Value = "Nigeria", Text = "Nigeria"},
                    new SelectListItem {Value = "Niue", Text = "Niue"},
                    new SelectListItem {Value = "Norfolk Island", Text = "Norfolk Island"},
                    new SelectListItem {Value = "North Korea", Text = "North Korea"},
                    new SelectListItem {Value = "Northern Cyprus", Text = "Northern Cyprus"},
                    new SelectListItem {Value = "Northern Mariana Islands", Text = "Northern Mariana Islands"},
                    new SelectListItem {Value = "Norway", Text = "Norway"},

                    new SelectListItem {Value = "Oman", Text = "Oman"},

                    new SelectListItem {Value = "Pakistan", Text = "Pakistan"},
                    new SelectListItem {Value = "Palau", Text = "Palau"},
                    new SelectListItem {Value = "Palestinian National Authority", Text = "Palestinian National Authority"},
                    new SelectListItem {Value = "Panama", Text = "Panama"},
                    new SelectListItem {Value = "Papua New Guinea", Text = "Papua New Guinea"},
                    new SelectListItem {Value = "Paraguay", Text = "Paraguay"},
                    new SelectListItem {Value = "Peru", Text = "Peru"},
                    new SelectListItem {Value = "Philippines", Text = "Philippines"},
                    new SelectListItem {Value = "Pitcairn Islands", Text = "Pitcairn Islands"},
                    new SelectListItem {Value = "Poland", Text = "Poland"},
                    new SelectListItem {Value = "Portugal", Text = "Portugal"},
                    new SelectListItem {Value = "Puerto Rico", Text = "Puerto Rico"},

                    new SelectListItem {Value = "Qatar", Text = "Qatar"},
                    new SelectListItem {Value = "Réunion", Text = "Réunion"},
                    new SelectListItem {Value = "Romania", Text = "Romania"},
                    new SelectListItem {Value = "Russia", Text = "Russia"},
                    new SelectListItem {Value = "Rwanda", Text = "Rwanda"},

                    new SelectListItem {Value = "Samoa", Text = "Samoa"},
                    new SelectListItem {Value = "San Marino", Text = "San Marino"},
                    new SelectListItem {Value = "São Tomé and Príncipe", Text = "São Tomé and Príncipe"},
                    new SelectListItem {Value = "Saudi Arabia", Text = "Saudi Arabia"},
                    new SelectListItem {Value = "Senegal", Text = "Senegal"},
                    new SelectListItem {Value = "Serbia", Text = "Serbia"},
                    new SelectListItem {Value = "Seychelles", Text = "Seychelles"},
                    new SelectListItem {Value = "Sierra Leone", Text = "Sierra Leone"},
                    new SelectListItem {Value = "Singapore", Text = "Singapore"},
                    new SelectListItem {Value = "Sint Maarten", Text = "Sint Maarten"},
                    new SelectListItem {Value = "Slovakia", Text = "Slovakia"},
                    new SelectListItem {Value = "Slovenia", Text = "Slovenia"},
                    new SelectListItem {Value = "Solomon Islands", Text = "Solomon Islands"},
                    new SelectListItem {Value = "Somalia", Text = "Somalia"},
                    new SelectListItem {Value = "Somaliland", Text = "Somaliland"},
                    new SelectListItem {Value = "South Africa", Text = "South Africa"},
                    new SelectListItem {Value = "South Korea", Text = "South Korea"},
                    new SelectListItem {Value = "South Sudan", Text = "South Sudan"},
                    new SelectListItem {Value = "South Ossetia", Text = "South Ossetia"},
                    new SelectListItem {Value = "Spain", Text = "Spain"},
                    new SelectListItem {Value = "Sri Lanka", Text = "Sri Lanka"},
                    new SelectListItem {Value = "Sudan", Text = "Sudan"},
                    new SelectListItem {Value = "Suriname", Text = "Suriname"},
                    new SelectListItem {Value = "Svalbard", Text = "Svalbard"},
                    new SelectListItem {Value = "Swaziland", Text = "Swaziland"},
                    new SelectListItem {Value = "Sweden", Text = "Sweden"},
                    new SelectListItem {Value = "Switzerland", Text = "Switzerland"},
                    new SelectListItem {Value = "Syria", Text = "Syria"},

                    new SelectListItem {Value = "Taiwan", Text = "Taiwan"},
                    new SelectListItem {Value = "Tajikistan", Text = "Tajikistan"},
                    new SelectListItem {Value = "Tanzania", Text = "Tanzania"},
                    new SelectListItem {Value = "Thailand", Text = "Thailand"},
                    new SelectListItem {Value = "Togo", Text = "Togo"},
                    new SelectListItem {Value = "Tokelau", Text = "Tokelau"},
                    new SelectListItem {Value = "Tonga", Text = "Tonga"},
                    new SelectListItem {Value = "Transnistria", Text = "Transnistria"},
                    new SelectListItem {Value = "Trinidad and Tobago", Text = "Trinidad and Tobago"},
                    new SelectListItem {Value = "Tunisia", Text = "Tunisia"},
                    new SelectListItem {Value = "Turkey", Text = "Turkey"},
                    new SelectListItem {Value = "Turkmenistan", Text = "Turkmenistan"},
                    new SelectListItem {Value = "Turks and Caicos Islands", Text = "Turks and Caicos Islands"},
                    new SelectListItem {Value = "Tuvalu", Text = "Tuvalu"},

                    new SelectListItem {Value = "Uganda", Text = "Uganda"},
                    new SelectListItem {Value = "Ukraine", Text = "Ukraine"},
                    new SelectListItem {Value = "United Arab Emirates", Text = "United Arab Emirates"},
                    new SelectListItem {Value = "United Kingdom", Text = "United Kingdom"},
                    new SelectListItem {Value = "United States", Text = "United States"},
                    new SelectListItem {Value = "United States Virgin Islands", Text = "United States Virgin Islands"},
                    new SelectListItem {Value = "Uruguay", Text = "Uruguay"},
                    new SelectListItem {Value = "Uzbekistan", Text = "Uzbekistan"},

                    new SelectListItem {Value = "Vanuatu", Text = "Vanuatu"},
                    new SelectListItem {Value = "Vatican City", Text = "Vatican City"},
                    new SelectListItem {Value = "Venezuela", Text = "Venezuela"},
                    new SelectListItem {Value = "Vietnam", Text = "Vietnam"},

                    new SelectListItem {Value = "Wallis and Futuna", Text = "Wallis and Futuna"},
                    new SelectListItem {Value = "Yemen", Text = "Yemen"},
                    new SelectListItem {Value = "Zambia", Text = "Zambia"},
                    new SelectListItem {Value = "Zimbabwe", Text = "Zimbabwe"}
                };
            }
        }
        [Display(Name = "Nationality")]
        [Required]
        public string nationality { get; set; }
        [Display(Name = "Mobile no.")]
        [Required]
        public string mobileNo { get; set; }
        [Display(Name = "Phone no.")]
        public string phoneNo { get; set; }
        [Display(Name = "Email address")]
        public string email { get; set; }

        [Display(Name = "ID Type")]
        public string SelectedIDtype { get; set; }
        public IEnumerable<SelectListItem> IDType
        {
            get
            {
                return new[]
                {
                    new SelectListItem {Value = "CREDIT CARD ID",Text = "CREDIT CARD ID" },
                    new SelectListItem {Value = "DRIVERS'S LICENSE", Text = "DRIVERS'S LICENSE"},
                    new SelectListItem {Value = "EMPLOYMENT ID", Text = "EMPLOYMENT ID"},
                    new SelectListItem {Value = "GSIS", Text = "GSIS"},
                    new SelectListItem {Value = "NBI CLEARANCE", Text = "NBI CLEARANCE"},
                    new SelectListItem {Value = "OTHER GOVERNMENT ISSUE", Text = "OTHER GOVERNMENT ISSUE"},
                    new SelectListItem {Value = "PASSPORT", Text = "PASSPORT"},
                    new SelectListItem {Value = "POLICE CLEARANCE", Text = "POLICE CLEARANCE"},
                    new SelectListItem {Value = "POSTAL ID", Text = "POSTAL ID"},
                    new SelectListItem {Value = "PRC", Text = "PRC"},
                    new SelectListItem {Value = "SCHOOL ID", Text = "SCHOOL ID"},
                    new SelectListItem {Value = "SENIOR CITIZEN ID", Text = "SENIOR CITIZEN ID"},
                    new SelectListItem {Value = "SSS", Text = "SSS"},
                    new SelectListItem {Value = "TAX IDENTIFICATION ID", Text = "TAX IDENTIFICATION ID"},
                    new SelectListItem {Value = "NEW VOTER'S ID", Text = "NEW VOTER'S ID"},
                    new SelectListItem {Value = "OTHERS", Text = "OTHERS"},
                };
            }
        }
        [Display(Name = "ID no")]
        [Required]
        public string Idno { get; set; }
        [Display(Name = "Expiry date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime? ExpiryDate { get; set; }
        public string custID { get; set; }
        public int isUpdate { get; set; }
    }
}