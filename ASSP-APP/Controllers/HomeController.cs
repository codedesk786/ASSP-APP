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
        public List<LoginModel> ObjLoginModel = new List<LoginModel>();
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

            LoginModel objRegistration = new LoginModel();

            objRegistration.Username = UserName;
            objRegistration.RememberMe = RememberME;
            return View(objRegistration);
        }


        [HttpPost]
        public ActionResult Login(LoginModel login)
        {
            string message = "";
            bool status = false;
            PopulateUsers();
            var verifylog = ObjLoginModel.Where(c => c.Username == login.Username && c.Password == login.Password).SingleOrDefault();
            if (verifylog != null)
            {
                status = true; // show 2FA form
                message = "2FA Verification";
                Session["UserName"] = login.Username;
                if (login.RememberMe)
                {

                    HttpCookie cookie = new HttpCookie("ASSP");
                    cookie.Values.Add("UserName", login.Username);
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
                string UserUniqueKey = (login.Username + key); //as Its a demo, I have done this way. But you should use any encrypted value here which will be unique value per user.
                Session["UserUniqueKey"] = UserUniqueKey;
                //This is used for enable 2factor authentication
                //var setupInfo = tfa.GenerateSetupCode("Dotnet Awesome", login.Username, UserUniqueKey, 300, 300);
                //ViewBag.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                //ViewBag.SetupCode = setupInfo.ManualEntryKey;
                if (verifylog.Enable2fact)
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
        public ActionResult MyProfile()
        {
            if (Session["Username"] == null || Session["IsValid2FA"] == null || !(bool)Session["IsValid2FA"])
            {
                return RedirectToAction("Login");
            }

            ViewBag.Message = "Welcome " + Session["Username"].ToString();
            return View();
        }
        [HttpPost]
        public ActionResult Verify2FA(FormCollection frmcollection)
        {
            var token = frmcollection["passcode"];
            TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
            string UserUniqueKey = Session["UserUniqueKey"].ToString();
            bool isValid = tfa.ValidateTwoFactorPIN(UserUniqueKey, token);
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


            LoginModel objLogin = new LoginModel();
            objLogin.Username = "Admin";
            objLogin.Password = "Password1";
            objLogin.Enable2fact = true;
            ObjLoginModel.Add(objLogin);

            LoginModel objLogin2 = new LoginModel();
            objLogin2.Username = "admin1";
            objLogin2.Password = "123";
            objLogin2.Enable2fact = true;
            ObjLoginModel.Add(objLogin2);

        }
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            return Redirect("/home/login");
        }


    }
}