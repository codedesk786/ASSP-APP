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

        private ASSPEntities db = new ASSPEntities();

        // GET: User
        public ActionResult adduser()

        {
            ViewBag.RadioButtonValues = db.Roles.ToList();

            return View();
        }
        public ActionResult InsertUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");

            }


            return View();
        }
    }
}