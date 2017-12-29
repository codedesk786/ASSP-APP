using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ASSP_APP.Models
{
    public class LoginModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public bool RememberMe { get; set; }
        public bool Enable2fact { get; set; }


       
    }
}