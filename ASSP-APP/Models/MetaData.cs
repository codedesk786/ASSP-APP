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
        public long UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PhoneNo { get; set; }
        public string Occupation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> Postcode { get; set; }
        public string Linkedin { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Instagram { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> UpdatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<bool> TwoFactor { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> RolID { get; set; }

        public virtual Role Role { get; set; }



    }

}