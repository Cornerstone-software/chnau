using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data;
using MvcFXProductMgr.Models;
namespace MvcFXProductMgr.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                ViewData["user"] = User.Identity.Name;

                var cookie = HttpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                string strrole = "操作员";
                if (User.Identity.Name.ToLower() == "admin") strrole = "管理员";
                ViewData["role"] = strrole;
                ViewData["ltime"] = ticket.IssueDate.ToString();
                
                //近期销售数据
                string strCommandText = "SELECT COUNT(P_Name) AS productSum, FORMAT(SUM(P_Weight),2) AS weightSum,P_Category AS category FROM p_info_table where P_Status='N'";
                strCommandText += " GROUP BY P_Category";
                DataTable dt = new DataTable();
                try
                {
                    dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                }catch(Exception ex){
                    HttpContext.Response.Write(ex.Message);
                }
                return View(dt);
                
            }
            else
            {
                return RedirectToAction("LogOn", "Account");
            }

        }

    }
}
