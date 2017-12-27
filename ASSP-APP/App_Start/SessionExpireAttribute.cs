using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace ASSP_APP.Controllers
{
    public class SessionExpireAttribute : ActionFilterAttribute
    {
      
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //HttpContext ctx = HttpContext.Current;
            //if (ctx.Session["UserTypeID"] != null)
            //{
            //    long UserTypeID = long.Parse(ctx.Session["UserTypeID"].ToString());
            //    int count = 0;
            //    var MenuList = db.RoleMenus.Where(user => user.UserTypeID == UserTypeID).ToList();
            //    string url = ctx.Request.Url.ToString();
            //    string[] test = url.Split('/');
            //    string type = ctx.Request.Url.ToString();
            //    string abPath = test[4] + "/" + test[5];

            //    var menuid = db.Menus.Where(m => m.MenuURL == abPath).ToList();
            //    foreach (var items in MenuList)
            //    {
            //        if (menuid.Any())
            //        {
            //            foreach (var mitems in menuid)
            //            {
            //                if (items.MenuID.Equals(mitems.MenuID))
            //                {
            //                    count = 1;
            //                }
            //            }
            //        }
            //    }
            //    if (count == 0 && menuid.Any())
            //    {
            //        filterContext.Result = new RedirectResult("~/home/PermissionDenied");
            //        return;
            //    }
            //}


            // check  sessions here
            if (HttpContext.Current.Session["UserName"] == null)
            {
                filterContext.Result = new RedirectResult("~/home/login");
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}