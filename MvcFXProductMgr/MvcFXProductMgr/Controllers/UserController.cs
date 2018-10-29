using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFXProductMgr.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /Account/

        //登录界面
        public ActionResult LogOn()
        {
            return View();
        }
        //退出
        public ActionResult LogOff()
        {
            return RedirectToAction("LogOn","User");
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        public bool DeleteAccount(string Name)
        {
            return true;
        }
    }
}
