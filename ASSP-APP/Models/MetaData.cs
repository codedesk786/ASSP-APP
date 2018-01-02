using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ASSP_APP.Models
{

    public class UserMetaData
    {
        public string UserID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; }
        public string RoleID{ get; set; }

    }

}