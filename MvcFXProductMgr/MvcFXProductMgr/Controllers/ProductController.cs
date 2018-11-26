using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using MvcFXProductMgr.Models;


namespace MvcFXProductMgr.Controllers
{
    public class ProductController : Controller
    {
        //
        // GET: /Product/

        public ActionResult GetProduct()
        {
            return View();
        }
        
        public ActionResult AddProducts()
        {
            return View();
        }
        public ActionResult DeleteProducts()
        {
            return View();
        }
        public ActionResult ChangeProducts()
        {
            return View();
        }

      [HttpPost] 
        public ActionResult Upload()
            
        {
          
            string strFileName = "";
            string strSeverDataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
            string strSaveFileName = "";
           
            HttpPostedFileBase uploadfile = Request.Files[0];
            string strCompany = Request["Company"];
            string[] arrCompany= strCompany.Split(':');
            string strCompanyId=arrCompany[0];
            string strCompanyName = arrCompany[1];

            string strTestingOrg = Request["TestingOrg"];
            string[] arrTestingOrg = strTestingOrg.Split(':');
            string strTestingOrgId=arrTestingOrg[0];
            string strTestingOrgName = arrTestingOrg[1];

            string strStandard = Request["Standard"];
            string strCategory = Request["Category"];
            
            Stream st = null;
            if (uploadfile != null && uploadfile.ContentLength > 0)
            {
                ///先保存上传的文件到server,begining
                strFileName = uploadfile.FileName;
                st = uploadfile.InputStream;
                strSaveFileName = strSeverDataPath + strFileName;
                if (!System.IO.File.Exists(strSaveFileName))
                {
                    uploadfile.SaveAs(strSaveFileName);
                }
                ///先保存上传的文件到server,ending

                ExcelHelper excelobj = new ExcelHelper(strSaveFileName);
                DataTable dt = excelobj.GetDataTable("sheet1", true);
                DataTable newdt = dt.Copy();

                DataColumn dcCompanyId = new DataColumn("CompanyId", typeof(string));
                dcCompanyId.DefaultValue = strCompanyId;
                newdt.Columns.Add(dcCompanyId);

                DataColumn dcCompanyName = new DataColumn("CompanyName", typeof(string));
                dcCompanyName.DefaultValue = strCompanyName;
                newdt.Columns.Add(dcCompanyName);

                DataColumn dcTestingOrgId = new DataColumn("TestingOrgId",typeof(Int32));
                dcTestingOrgId.DefaultValue = Int32.Parse(strTestingOrgId);
                newdt.Columns.Add(dcTestingOrgId);
                DataColumn dcTestingOrgName = new DataColumn("TestingOrgName", typeof(string));
                dcTestingOrgName.DefaultValue = strTestingOrgName;
                newdt.Columns.Add(dcTestingOrgName);

                DataColumn dcCategory = new DataColumn("Category", typeof(string));
                dcCategory.DefaultValue = strCategory;
                newdt.Columns.Add(dcCategory);

                DataColumn dcStandard = new DataColumn("Standard", typeof(string));
                dcStandard.DefaultValue = strStandard;
                newdt.Columns.Add(dcStandard);

                //ViewData.Model = newdt.AsEnumerable();
                return View(newdt);
               

            }
            else
            {
                return Content("Empty excel");
            }       
                
        }
    [HttpPost]
      public ActionResult Save()
      {
         
          return RedirectToAction("AddProducts","Product");
      }
    }
}
