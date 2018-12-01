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
using MySql.Data.MySqlClient;
using MvcFXProductMgr.Models;

namespace MvcFXProductMgr.Controllers
{
    public class ProductController : Controller
    {
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
            string[] arrCompany = strCompany.Split(':');
            string strCompanyId = arrCompany[0];
            string strCompanyName = arrCompany[1];

            string strTestingOrg = Request["TestingOrg"];
            string[] arrTestingOrg = strTestingOrg.Split(':');
            string strTestingOrgId = arrTestingOrg[0];
            string strTestingOrgName = arrTestingOrg[1];

            string strStandard = Request["Standard"];
            //处理特殊字符，以免和SaveProducts的Form数据冲突
            strStandard = strStandard.Replace(',', ':');
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
               
                //修改列名,使Excel列名和Model属性一致
                foreach (DataColumn dc in newdt.Columns)
                {
                    string colName = dc.ColumnName;
                    if (colName.Contains('_')) {
                        string[] strTemp = colName.Split('_');
                        if (strTemp.Length > 0)
                        {
                            dc.ColumnName = strTemp[1];
                        }
                    }
                    else
                    {
                        if (colName == "证书编号") dc.ColumnName = "CerNum";
                        if (colName == "条码号") dc.ColumnName = "Barcode";
                        if (colName == "品名") dc.ColumnName = "Name";
                        if (colName == "重量") dc.ColumnName = "Weight";
                        if (colName == "售价") dc.ColumnName = "Price";                      
                    }
                    
                }
                DataColumn dcCompanyId = new DataColumn("CId", typeof(string));
                dcCompanyId.DefaultValue = strCompanyId;
                newdt.Columns.Add(dcCompanyId);

                DataColumn dcCompanyName = new DataColumn("CName", typeof(string));
                dcCompanyName.DefaultValue = strCompanyName;
                newdt.Columns.Add(dcCompanyName);

                DataColumn dcTestingOrgId = new DataColumn("TId", typeof(Int32));
                dcTestingOrgId.DefaultValue = Int32.Parse(strTestingOrgId);
                newdt.Columns.Add(dcTestingOrgId);
                DataColumn dcTestingOrgName = new DataColumn("TName", typeof(string));
                dcTestingOrgName.DefaultValue = strTestingOrgName;
                newdt.Columns.Add(dcTestingOrgName);

                DataColumn dcCategory = new DataColumn("Category", typeof(string));
                dcCategory.DefaultValue = strCategory;
                newdt.Columns.Add(dcCategory);

                DataColumn dcStandard = new DataColumn("Standard", typeof(string));
                dcStandard.DefaultValue = strStandard;
                newdt.Columns.Add(dcStandard);

                List<ProductModel> list = new List<ProductModel>();
                list = ConvertHelper<ProductModel>.DataTableToList(newdt);
                return View(list);


            }
            else
            {
                return Content("Empty excel");
            }       
                
        }

        /// <summary>
        /// 批量上已读取的Excel数据到MySQL DataBase
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
          public ActionResult SaveProducts()
          {
              if (ModelState.IsValid)
              {                 
                  string strName = Request["Name"].ToString();
                  string strWeight = Request["Weight"].ToString();
                  string strCerNum = Request["CerNum"].ToString();
                  string strBarcode = Request["Barcode"].ToString();
                  string strPrice = Request["Price"].ToString();
                  string strStandard = Request["Standard"].ToString();
                  string strCId = Request["CId"].ToString();
                  string strTId = Request["TId"].ToString();
                  string strCategory = Request["Category"].ToString();
                  string[] arrName = strName.Split(',');
                  string[] arrWeight = strWeight.Split(',');

                  string[] arrCerNum = strCerNum.Split(',');
                  string[] arrBarcode = strBarcode.Split(',');

                  string[] arrPrice = strPrice.Split(',');
                  string[] arrStandard = strStandard.Split(',');
                  string[] arrCId = strCId.Split(',');
                  string[] arrTId = strTId.Split(',');
                  string[] arrCategory = strCategory.Split(',');
                  //创建DataTable
                  DataTable dt = new DataTable();
                  DataColumn dcName = new DataColumn("Name", typeof(string));
                  DataColumn dcWeight = new DataColumn("Weight", typeof(string));
                  DataColumn dcCerNum = new DataColumn("CerNum", typeof(string));
                  DataColumn dcBarcode = new DataColumn("Barcode", typeof(string));
                  DataColumn dcPrice = new DataColumn("Price", typeof(string));
                  DataColumn dcStandard = new DataColumn("Standard", typeof(string));
                  DataColumn dCId = new DataColumn("CId", typeof(string));
                  DataColumn dcTId = new DataColumn("TId", typeof(string));
                  DataColumn dcCategory = new DataColumn("Category", typeof(string));
                  dt.Columns.Add(dcName);
                  dt.Columns.Add(dcWeight);
                  dt.Columns.Add(dcCerNum);
                  dt.Columns.Add(dcBarcode);
                  dt.Columns.Add(dcPrice);
                  dt.Columns.Add(dcStandard);
                  dt.Columns.Add(dCId);
                  dt.Columns.Add(dcTId);
                  dt.Columns.Add(dcCategory);
                  for (int i = 0; i < arrName.Length; i++)
                  {
                      DataRow dr = dt.NewRow();
                      dr["Name"] = arrName[i]??"";
                      dr["Weight"] = arrWeight[i] ?? "";
                      dr["CerNum"] = arrCerNum[i] ?? "";
                      dr["Barcode"] = arrBarcode[i] ?? "";
                      dr["Price"] = arrPrice[i] ?? "";
                      dr["Standard"] = arrStandard[i] ?? "";
                      dr["CId"] = arrCId[i] ?? "";
                      dr["TId"] = arrTId[i] ?? "";
                      dr["Category"] = arrCategory[i] ?? "";
                      dt.Rows.Add(dr);
                  }
                  string strCommandText = "INSERT INTO p_info_table (P_Name,P_Weight,P_CerNum,P_Barcode,P_Price,P_Standard,P_Category,P_CId,P_Tid) VALUES(@Name,@Weight,@CerNum,@Barcode,@Price,@Standard,@Category,@CId,@TId)";
                  MySqlParameter[] commadparameters = {
                          new MySqlParameter("@Name",MySqlDbType.VarChar,100,"Name"),
                          new MySqlParameter("@Weight",MySqlDbType.Float,100,"Weight"),
                          new MySqlParameter("@CerNum",MySqlDbType.VarChar,100,"CerNum"),
                          new MySqlParameter("@Barcode",MySqlDbType.VarChar,100,"Barcode"),
                          new MySqlParameter("@Price",MySqlDbType.Int32,100,"Price"),
                          new MySqlParameter("@Standard",MySqlDbType.VarChar,100,"Standard"),
                          new MySqlParameter("@Category",MySqlDbType.VarChar,100,"Category"),
                          new MySqlParameter("@CId",MySqlDbType.Int32,100,"CId"),
                          new MySqlParameter("@TId",MySqlDbType.Int32,100,"TId")
                          };
                          if (dt != null)
                          {
                              bool bda = MySQLHelper.ExecuteDataAdapterBatch(MySQLHelper.Conn, CommandType.Text, strCommandText, dt, 5000, commadparameters);
                              if (bda)
                              {
                                  return RedirectToAction("SaveProductsSuccess", "Product");
                              }
                              
                          }
                  //return Content(Request["Standard"].ToString());
                          return Content("导入不成功");
              }
              else
              {
                  return Content("导入不成功");
              }
          
          }
          public ActionResult SaveProductsSuccess()
          {
         
              return View();
          }
    }
}
