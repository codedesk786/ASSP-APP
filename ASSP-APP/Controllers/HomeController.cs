using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Google.Authenticator;
using ASSP_APP.Models;
namespace ASSP_APP.Controllers
{
    public class HomeController : Controller
    {
        private const string key = "qaz123!@@)(*";
        List<DummyData> objDatlist = new List<DummyData>();
        Meta objmeta = new Meta();
        public List<User> ObjUserList = new List<User>();
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult login()
        {

            bool RememberME = false;
            string UserName = "";
            if (Request.Cookies["ASSP"] != null)
            {
                string username = Request.Cookies["ASSP"].Values["UserName"];

                UserName = username;
                RememberME = true;
            };

            User objRegistration = new User();

            objRegistration.UserName = UserName;
            objRegistration.RememberMe = RememberME;
            return View(objRegistration);
        }
        [HttpPost]
        public ActionResult Login(User login, FormCollection frm)
        {

            string message = "";
            bool status = false;
            if (frm["RememberMe"] != null)
            {
                login.RememberMe = true;
            }
            else
            {
                login.RememberMe = false;
            }
            PopulateUsers();
            var verifylog = ObjUserList.Where(c => c.UserName == login.UserName && c.Password == login.Password).SingleOrDefault();
            if (verifylog != null)
            {
                status = true; // show 2FA form
                message = "2FA Verification";
                Session["UserName"] = login.UserName;
                Session["RoleID"] = login.RolID;

                if (login.RememberMe)
                {

                    HttpCookie cookie = new HttpCookie("ASSP");
                    cookie.Values.Add("UserName", login.UserName);
                    cookie.Expires = DateTime.Now.AddDays(15);
                    Response.Cookies.Add(cookie);
                }
                else
                {
                    HttpCookie myCookie = new HttpCookie("ASSP");
                    myCookie.Expires = DateTime.Now.AddDays(-1d);
                    Response.Cookies.Add(myCookie);
                }

                //2FA Setup
                TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
                string UserUniqueKey = (login.UserName + key); //as Its a demo, I have done this way. But you should use any encrypted value here which will be unique value per user.
                Session["UserUniqueKey"] = UserUniqueKey;
                //This is used for enable 2factor authentication
                //var setupInfo = tfa.GenerateSetupCode("Dotnet Awesome", login.Username, UserUniqueKey, 300, 300);
                //ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                //ViewBag.SetupCode = setupInfo.ManualEntryKey;
                if (verifylog.TwoFactor.Value)
                {
                    return Content("enable2fact");
                }
                else
                {

                    return Content("success");
                }



            }
            else
            {
                return Content("error");
            }
            ViewBag.Message = message;
            ViewBag.Status = status;
            return View();
        }
        [SessionExpire]
        public ActionResult profile()
        {
            //if (Session["Username"] == null || Session["IsValid2FA"] == null || !(bool)Session["IsValid2FA"])
            //{
            //    return RedirectToAction("Login");
            //}

            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = (Session["UserName"].ToString() + key); //as Its a demo, I have done this way. But you should use any encrypted value here which will be unique value per user.
            Session["UserUniqueKey"] = UserUniqueKey;
            // This is used for enable 2factor authentication
            var setupInfo = tfa.GenerateSetupCode("Dotnet Awesome", Session["UserName"].ToString(), UserUniqueKey, 300, 300);
            ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            ViewBag.SetupCode = setupInfo.ManualEntryKey;
            ViewBag.Message = "Welcome " + Session["Username"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Verify2FA(FormCollection frmcollection)
        {
            var token = frmcollection["passcode"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = Session["UserUniqueKey"].ToString();
            TimeSpan obj = new TimeSpan();
            bool isValid = tfa.ValidateTwoFactorPIN(UserUniqueKey, token, obj);
            if (isValid)
            {
                Session["IsValid2FA"] = true;
                return Content("success");
            }
            else
            {

                return Content("error");
            }

        }
        [SessionExpire]
        public ActionResult dashboard()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void PopulateUsers()
        {


            User objLogin = new User();
            objLogin.UserName = "BP000001";
            objLogin.Password = "Password1";
            objLogin.RolID = 1;
            objLogin.TwoFactor = true;
            ObjUserList.Add(objLogin);

            User objLogin2 = new User();
            objLogin2.UserName = "BP0000002";
            objLogin2.Password = "123";
            objLogin2.TwoFactor = false;
            ObjUserList.Add(objLogin2);

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Redirect("/home/login");
        }

        public JsonResult LoadOrders(FormCollection frm)
        {

            string UserName = Session["UserName"].ToString();
            LoadList();

            objmeta.page = Convert.ToInt32(frm["datatable[pagination][page]"]);
            objmeta.pages = objDatlist.ToList().Count / Convert.ToInt32(frm["datatable[pagination][perpage]"]);
            objmeta.perpage = Convert.ToInt32(frm["datatable[pagination][perpage]"]);
            objmeta.total = objDatlist.ToList().Count;
            objmeta.sort = frm["datatable[sort][sort]"];
            objmeta.field = frm["datatable[sort][field]"];
            List<DummyData> objjson = new List<DummyData>();

            if (Session["RoleID"].ToString() == "1")
            {


                if (objmeta.sort == "asc")
                {
                    if (objmeta.field == "ServiceOrders")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.ServiceOrders).ToList();
                    }
                    else if (objmeta.field == "BusinessPartnerCode")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.BusinessPartnerCode).ToList();
                    }

                }
                else
                {


                    if (objmeta.field == "ServiceOrders")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.ServiceOrders).ToList();
                    }
                    else if (objmeta.field == "BusinessPartnerCode")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.BusinessPartnerCode).ToList();
                    }
                }
            }
            else
            {
                if (objmeta.sort == "asc")
                {
                    if (objmeta.field == "ServiceOrders")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.ServiceOrders).Where(c => c.UserName == UserName).ToList();
                    }
                    else if (objmeta.field == "BusinessPartnerCode")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderBy(c => c.BusinessPartnerCode).Where(c => c.UserName == UserName).ToList();
                    }

                }
                else
                {


                    if (objmeta.field == "ServiceOrders")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.ServiceOrders).Where(c => c.UserName == UserName).ToList();
                    }
                    else if (objmeta.field == "BusinessPartnerCode")
                    {
                        objjson = objDatlist.Skip((objmeta.page - 1) * objmeta.perpage).Take(objmeta.perpage).OrderByDescending(c => c.BusinessPartnerCode).Where(c => c.UserName == UserName).ToList();
                    }
                }

            }

            var jsonData = new
            {
                meta = objmeta,
                data = objjson
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);


        }
        public void LoadList()
        {

            for (int i = 1; i < 5; i++)
            {
                DummyData objDummyData = new DummyData();
                objDummyData.ServiceOrders = "Ser000" + i;

                objDummyData.BusinessPartnerCode = "BP00000" + i;
                objDummyData.BPName = "xyz";
                objDummyData.Configuration = "CONF000000" + i;
                objDummyData.ServiceLocationAddress = "Rotterdam Holland";
                objDummyData.OrderDate = DateTime.Now;
                objDummyData.EstimatedStartDate = DateTime.Now;
                objDummyData.EstimatedEndDate = DateTime.Now;
                objDummyData.Duration = "5  hours";
                objDummyData.ActualStartDate = DateTime.Now;
                objDummyData.ActualFinishDate = DateTime.Now;
                objDummyData.OrderStatus = "Free";
                objDummyData.ReasonforIntruption = "Waiting for parts delivery from Supplier";
                objDummyData.ExpectedDeliveryDate = DateTime.Now;
                objDummyData.ServiceDepartment = "SER-Norway";
                objDummyData.ServiceEngineer = "Christofer";
                objDummyData.ServiceManager = "Ola Norman";
                objDummyData.ViewDocuments = "www.google.com";
                objDatlist.Add(objDummyData);
            }

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


        public class Datum
        {
            public int RecordID { get; set; }
            public string OrderID { get; set; }
            public string ShipCountry { get; set; }

        }

        public class RootObject
        {
            public Meta meta { get; set; }
            public List<Datum> data { get; set; }
        }
        [HttpPost]
        public string Forgetpassword(FormCollection frmCollection)
        {
            string msgtype = "";
            string email = frmCollection["email"];
            User obj = ObjUserList.Where(u => u.Email == email).SingleOrDefault();
            if (obj == null)
            {
                msgtype = "danger";
            }
            else
            {

                //SendEmail objSendEmail = new SendEmail { Subject = "Password Recovery" };

                //objSendEmail.ReceiverAddress = obj.UserName;
                //string password = obj.Password;

                //string htmlBody = string.Empty;
                //string filePath = Server.MapPath(Url.Content("~/Content/Templates/ForgetPassword.txt"));

                //using (System.IO.StreamReader sr = System.IO.File.OpenText(filePath))
                //{
                //    htmlBody = sr.ReadToEnd();
                //    sr.Close();
                //}
                //htmlBody = htmlBody.Replace("{URL}", WebConfigurationManager.AppSettings["URL"] + "Content\\");
                //htmlBody = htmlBody.Replace("{link}", WebConfigurationManager.AppSettings["URL"] + "home/login");
                //htmlBody = htmlBody.Replace("{Name}", obj.FullName);
                //htmlBody = htmlBody.Replace("{Email}", obj.UserName);
                //htmlBody = htmlBody.Replace("{Password}", EncryptDecrypt.Decrypt(obj.Password, true));
                //objSendEmail.Body = htmlBody;
                //objSendEmail
                msgtype = "success";
            }
            return msgtype;
        }
        public class DummyData
        {
            public string UserName { get; set; }
            public string ServiceOrders { get; set; }
            public string BusinessPartnerCode { get; set; }
            public string BPName { get; set; }
            public string Configuration { get; set; }
            public string ServiceLocationAddress { get; set; }
            public DateTime OrderDate { get; set; }
            public DateTime EstimatedStartDate { get; set; }
            public DateTime EstimatedEndDate { get; set; }
            public string Duration { get; set; }
            public DateTime ActualStartDate { get; set; }
            public DateTime ActualFinishDate { get; set; }
            public string OrderStatus { get; set; }
            public string ReasonforIntruption { get; set; }
            public DateTime ExpectedDeliveryDate { get; set; }
            public string ServiceDepartment { get; set; }
            public string ServiceEngineer { get; set; }
            public string ServiceManager { get; set; }
            public string ViewDocuments { get; set; }
        }
    }

}