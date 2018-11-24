using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcFXProductMgr.Controllers
{
    public class CompanyController : Controller
    {
        //
        // GET: /Company/

        public ActionResult Index()
        {
            //点击公司信息树状态菜单，自动加载列表显示现在存在的所有公司信息，公司信息后带修改，删除功能，列表可以批量删除，列表下方做增加新的公司链接。如果无公司信息，则提示公司信息还未初始，请添加公司信息，做增加新公司链接，跳转至新建公司信息页面。

            return View();
        }

        //
       
        /// <summary>
        /// 创建新的公司信息
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateNewCompany()
        {
          
            ViewBag.Title = "新建公司信息";


            string strCompanyName = "";
            string strCompanyAddress = "";
            string strCompanyUrl = "";
            string strCompanyTel = "";
            //判断是否有客户提交的公司信息，如果没有，第一次访问新建公司模块，刚显示添加公司信息页面，如果有信息，则说明客户已经输入了客户信息，需要调用方法来增加公司信息至数据库，并显示添加已经成功。
            
            return View("CreateOrUpdateCompany");
        }

        public ActionResult UpdateCompany()
        {

            ViewBag.Title = "更新公司信息";
            return View("CreateOrUpdateCompany");
        }
        public ActionResult DedelteCompany()
        {
            ViewBag.Title = "删除公司信息";
            return View();
        }

    }
}
