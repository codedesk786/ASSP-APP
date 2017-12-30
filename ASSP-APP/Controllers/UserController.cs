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
        Meta objmeta = new Meta();
        List<User> objDatlist = new List<User>();
        // GET: User
        public ActionResult adduser()
        {
            ViewBag.RadioButtonValues = db.Roles.ToList();

            return View();
        }
        public ActionResult AllEmployee()
        {

            return View();
        }
        public JsonResult GetAllEmployees(FormCollection frm)
        {
            objDatlist =  db.Users.ToList();
            objmeta.page = Convert.ToInt32(frm["datatable[pagination][page]"]);
            objmeta.pages = objDatlist.ToList().Count / Convert.ToInt32(frm["datatable[pagination][perpage]"]);
            objmeta.perpage = Convert.ToInt32(frm["datatable[pagination][perpage]"]);
            objmeta.total = objDatlist.ToList().Count;
            objmeta.sort = frm["datatable[sort][sort]"];
            objmeta.field = frm["datatable[sort][field]"];
            List<User> objjson = new List<User>();

            if (Session["RoleID"].ToString() == "1")
            {


                if (objmeta.sort == "asc")
                {
                    if (objmeta.field == "UserName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.UserName).ToList();
                    }
                    else if (objmeta.field == "UserID")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.UserID).ToList();
                    }

                }
                else
                {


                    if (objmeta.field == "UserName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.UserName).ToList();
                    }
                    else if (objmeta.field == "UserName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.UserID).ToList();
                    }
                }
            }
            else
            {
                if (objmeta.sort == "asc")
                {
                    if (objmeta.field == "UserName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.UserName).ToList();
                    }
                    else if (objmeta.field == "UserID")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.UserID).ToList();
                    }

                }
                else
                {


                    if (objmeta.field == "UserName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.UserName).ToList();
                    }
                    else if (objmeta.field == "UserName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.UserID).ToList();
                    }
                }

            }

            List<UserMetaData> objuserMeta = new List<UserMetaData>();
            foreach (var item in objjson)
            {
                UserMetaData objUser = new UserMetaData();
                objUser.FullName = item.FullName;
                objUser.UserName = item.UserName;
                objuserMeta.Add(objUser);
            }
            var jsonData = new
            {
                meta = objmeta,
                data= objuserMeta
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
        public string InsertUser(User user)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var verifyUserName = db.Users.Where(c => c.UserName == user.UserName).SingleOrDefault();
                if (verifyUserName != null)
                {
                    message = "UserAlreadyExists";
                }
                else
                {
                    db.Users.Add(user);
                    db.SaveChanges();
                    message = "success";
                }


            }
            return message;
        }
        public class Meta
        {
            public int page { get; set; }
            public int pages { get; set; }
            public int perpage { get; set; }
            public int total { get; set; }
            public string sort { get; set; }
            public string field { get; set; }
        }
    }
}