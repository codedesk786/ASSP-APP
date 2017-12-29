using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASSP_APP.Models
{

    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        public bool RememberMe { get; set; }
    }


}