using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Authenticator;
using ASSP_APP.Models;

namespace ASSP_APP.Controllers
{
    public class UserController : Controller
    {
<<<<<<< HEAD
        private ASSPEntities db = new ASSPEntities();
=======
        private AsspDB db = new AsspDB();
>>>>>>> 7f361729336fc9a88fcea07a5618e1092972419d
        // GET: User
        public ActionResult adduser()
        {
            return View();
        }
        public ActionResult InsertUser(string FullName, string UserName,string Password,string Address,int RoleID)
        {
            if(RoleID != null)
            {
                User objUser = new User();
                objUser.FullName = FullName;
                objUser.UserName = UserName;
                objUser.Password = Password;
                objUser.Address1 = Address;
                objUser.RolID = RoleID;
                db.Users.Add(objUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}