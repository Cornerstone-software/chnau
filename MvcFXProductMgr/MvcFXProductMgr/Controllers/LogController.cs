using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFXProductMgr.Models;
using MvcFXProductMgr.ViewModels;
namespace MvcFXProductMgr.Controllers
{
    public class LogController : Controller
    {
        //
        // GET: /Log/

        public ActionResult Index()
        {
            LogModel model = new LogModel();
            LogViewModel viewModel = new LogViewModel();
            string strUserName = User.Identity.Name ?? "";
            viewModel = model.GetLogsByName(User.Identity.Name);
            return View(viewModel);
        }

    }
}
