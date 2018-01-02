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
            ViewBag.RadioButtonValues = db.Roles.ToList();
            return View();
        }
        public JsonResult GetAllEmployees(FormCollection frm)
        {
            objDatlist = db.Users.ToList();
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
                    if (objmeta.field == "FullName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.UserName).ToList();
                    }
                    else if (objmeta.field == "FullName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.UserID).ToList();
                    }

                }
                else
                {


                    if (objmeta.field == "FullName")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.UserName).ToList();
                    }
                    else if (objmeta.field == "FullName")
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
                objUser.Password = item.Password;
                objUser.Address = item.Address1;
                objUser.RoleName = item.Role.RoleName;
                objuserMeta.Add(objUser);
            }
            var jsonData = new
            {
                meta = objmeta,
                data = objuserMeta
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEmployeeByID(string UserName)
        {
            var UserID = db.Users.Where(u => u.UserName == UserName).FirstOrDefault().UserID;
            var user = db.Users.Find(UserID);
            if (user != null)
            {
                db.Users.Remove(user);
                db.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            else
                return Json("error", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult GetEmployeeByID(string UserName)
        {
            try
            {
                var Employee = db.Users.FirstOrDefault(u => u.UserName == UserName);
                List<UserMetaData> objuserMeta = new List<UserMetaData>();

                UserMetaData objUser = new UserMetaData();
                objUser.FullName = Employee.FullName;
                objUser.UserName = Employee.UserName;
                objUser.Password = Employee.Password;
                objUser.Address = Employee.Address1;
                objUser.RoleID = Convert.ToString(Employee.RoleID);
                objUser.UserID = Convert.ToString(Employee.UserID);
                objuserMeta.Add(objUser);

                var jsonData = new
                {
                    meta = objmeta,
                    data = objuserMeta
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }

        [HttpPost]
        public JsonResult GetEmployeeByUserID()
        {
            try
            {
                var UserID = Convert.ToInt32(Session["UserID"]);
                var Employee = db.Users.FirstOrDefault(u => u.UserID == UserID);
                List<User> objuserMeta = new List<User>();

                User objUser = new User();
                objUser.FullName = Employee.FullName;
                objUser.UserName = Employee.UserName;
                objUser.Occupation = Employee.Occupation;
                objUser.Address = Employee.Address2;
                objUser.CompanyName = Employee.CompanyName;
                objUser.PhoneNo = Employee.PhoneNo;
                objUser.State = Employee.State;
                objUser.City = Employee.City;
                objUser.Postcode = Employee.Postcode;
                objUser.Linkedin = Employee.Linkedin;
                objUser.Facebook = Employee.Facebook;
                objUser.Twitter = Employee.Twitter;
                objUser.Instagram = Employee.Instagram;
                objuserMeta.Add(objUser);

                var jsonData = new
                {
                    meta = objmeta,
                    data = objuserMeta
                };
                return Json(jsonData, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null;
            }
        }
        public string UpdateEmployeeByUserID(User user)
        {
            string msg = "success";
            try
            {
                int UserID = Convert.ToInt32(Session["UserID"]);
                var objUser = db.Users.Single(c => c.UserID == UserID);
                objUser.FullName = user.FullName;
                objUser.Occupation = user.Occupation;
                objUser.Address = user.Address2;
                objUser.CompanyName = user.CompanyName;
                objUser.PhoneNo = user.PhoneNo;
                objUser.State = user.State;
                objUser.City = user.City;
                objUser.Postcode = user.Postcode;
                objUser.Linkedin = user.Linkedin;
                objUser.Facebook = user.Facebook;
                objUser.Twitter = user.Twitter;
                objUser.Instagram = user.Instagram;
                db.SaveChanges();

                return msg;
            }
            catch
            {
                msg = "fail";
                return msg;
            }
        }
        public string UpdateEmployeeByUserName(User user)
        {
            string msg = "success";
            try
            {
                var verifyUserName = db.Users.Where(c => c.UserID == user.UserID).SingleOrDefault().UserName;
                if (verifyUserName != null && verifyUserName != user.UserName)
                {
                    msg = "UserAlreadyExists";
                }
                else
                {
                    int UserID = Convert.ToInt32(user.UserID);
                    var objUser = db.Users.Single(c => c.UserID == UserID);
                    objUser.UserName = user.UserName;
                    objUser.FullName = user.FullName;
                    objUser.Password = user.Password;
                    objUser.Address1 = user.Address1;
                    objUser.RoleID = user.RoleID;
                    db.SaveChanges();
                }
                return msg;
            }
            catch
            {
                msg = "fail";
                return msg;
            }
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
                    Response.Redirect("User/AllEmployee");
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