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
            Stream st = null;
            if (uploadfile != null && uploadfile.ContentLength > 0)
            {
                strFileName = uploadfile.FileName;
                st = uploadfile.InputStream;
                strSaveFileName = strSeverDataPath + strFileName;
                if (!System.IO.File.Exists(strSaveFileName))
                {
                    uploadfile.SaveAs(strSaveFileName);
                }
                
                ExcelHelper excelobj = new ExcelHelper(strSaveFileName);
                DataTable dt = excelobj.GetDataTable("sheet1", true);
                return Content(dt.Rows.Count.ToString());
            }
            else
            {
                return Content("Empty excel");
            }

          
            
           
                
        }
    }
}
