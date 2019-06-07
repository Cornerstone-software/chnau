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
using System.Diagnostics;
using MySql.Data.MySqlClient;
using MvcFXProductMgr.Models;
using MvcFXProductMgr.ViewModels;
namespace MvcFXProductMgr.Controllers
{
    public class ProductController : Controller
    {
       /// <summary>
        /// 获取指定产品的详细信息
       /// </summary>
       /// <param name="cerNum"></param>
       /// <param name="barcode"></param>
       /// <returns></returns>
        public ActionResult GetProduct(string cerNum, string barcode)
        {

            ProductModel model = new ProductModel();
            model = model.GetProduct(cerNum, barcode); 
            
            //设置logo Image的名称
            string strBrandImgPath = "../../Content/images/";
            string strCName = model.CName;
            if (model.CName.Contains("中国黄金"))
                strBrandImgPath += "LogoForChinaGold.png";
            else if (model.CName.Contains("日月明牌"))
                strBrandImgPath += "LogoForMP.png";
            else strBrandImgPath += "Logo-large.png";
            ViewData["ImgPath"] = strBrandImgPath;
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProducts()
        {
            //公司列表下拉框信息
            CompanyModel modelForCompany = new CompanyModel();
            List<SelectListItem> itemsForCompany = new List<SelectListItem>();
            List<CompanyModel> clist = modelForCompany.GetAllCompanys();
            //增加“所有公司”到公司信息列表
            SelectListItem selectItemAForCompany = new SelectListItem();
            selectItemAForCompany.Text = "---所有公司---";
            selectItemAForCompany.Value = "0" + ":" + "所有公司";
            itemsForCompany.Add(selectItemAForCompany);
            foreach (CompanyModel citem in clist)
            {
                SelectListItem selectItemForCompany = new SelectListItem();
                selectItemForCompany.Text = citem.Name;
                selectItemForCompany.Value = citem.Id.ToString() + ":" + citem.Name;
                if (clist.IndexOf(citem) == 0) selectItemForCompany.Selected = true;
                itemsForCompany.Add(selectItemForCompany);
            }
            this.ViewData["Company"] = itemsForCompany;
            return View();
        }
        /// <summary>
        /// 按照查询条件获取产品信息
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductsBy(FormCollection collection)
        {
            string strcategory = collection["Category"];
            string strCompany = collection["Company"];
            string[] arrCompany = strCompany.Split(':');
            int iCId = Int32.Parse(arrCompany[0]);
            string strStartDate = collection["StartDate"];
            string strEndDate = collection["EndDate"];
            strStartDate += " 00:00:00";
            strEndDate += " 23:59:59";
            ProductModel model= new ProductModel();
            List<ProductModel> list = model.GetProductsBy(strcategory,iCId,strStartDate,strEndDate);
            return View(list);
        }
        /// <summary>
        /// 批量上传界面信息设置
        /// </summary>
        /// <returns></returns>
        public ActionResult AddProducts()
        {
            //公司列表下拉框信息
            CompanyModel modelForCompany = new CompanyModel();
            List<SelectListItem> itemsForCompany = new List<SelectListItem>();
            List<CompanyModel> clist = modelForCompany.GetAllCompanys();
            //增加数据库设定的公司信息
            foreach(CompanyModel citem in clist){
                SelectListItem selectItemForCompany = new SelectListItem();
                selectItemForCompany.Text = citem.Name;
                selectItemForCompany.Value = citem.Id.ToString() + ":" + citem.Name;
                if (clist.IndexOf(citem) == 0) selectItemForCompany.Selected = true;
                itemsForCompany.Add(selectItemForCompany);
            }
            this.ViewData["Company"] = itemsForCompany;

            //检测机构列表信息
            TestingOrgModel modelForTestingOrg = new TestingOrgModel();
            List<SelectListItem> itemsForTestingOrg = new List<SelectListItem>();
            List<TestingOrgModel> listForTestingOrg = modelForTestingOrg.GetAllTestingOrgs();
            foreach (TestingOrgModel itemForTestingOrg in listForTestingOrg)
            {
                SelectListItem selectItemForTestingOrg = new SelectListItem();
                selectItemForTestingOrg.Text = itemForTestingOrg.Name;
                selectItemForTestingOrg.Value = itemForTestingOrg.Id.ToString() + ":" + itemForTestingOrg.Name;
                if (listForTestingOrg.IndexOf(itemForTestingOrg) == 0) selectItemForTestingOrg.Selected = true;
                itemsForTestingOrg.Add(selectItemForTestingOrg);
            }
            this.ViewData["TestingOrg"] = itemsForTestingOrg;
            return View();
        }
        #region
        /// <summary>
        /// 显示已更改的信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UpdateProducts(List<ProductModel> model)
        {
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult UpdateProducts()
        {
            return View();
        }
        /// <summary>
        /// 显示要更改的产品列表
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetProductsForUpdate(FormCollection collection)
        {
            //公司列表下拉框信息
            CompanyModel modelForCompany = new CompanyModel();
            List<SelectListItem> itemsForCompany = new List<SelectListItem>();
            List<CompanyModel> clist = modelForCompany.GetAllCompanys();
            //增加“所有公司”到公司信息列表
            SelectListItem selectItemAForCompany = new SelectListItem();
            foreach (CompanyModel citem in clist)
            {
                SelectListItem selectItemForCompany = new SelectListItem();
                selectItemForCompany.Text = citem.Name;
                selectItemForCompany.Value = citem.Id.ToString() + ":" + citem.Name;
                if (collection["CName"].Contains(citem.Name)) selectItemForCompany.Selected = true;
                itemsForCompany.Add(selectItemForCompany);
            }
            ViewData["Company"] = itemsForCompany;
            //检测机构列表信息
            TestingOrgModel modelForTestingOrg = new TestingOrgModel();
            List<SelectListItem> itemsForTestingOrg = new List<SelectListItem>();
            List<TestingOrgModel> listForTestingOrg = modelForTestingOrg.GetAllTestingOrgs();
            foreach (TestingOrgModel itemForTestingOrg in listForTestingOrg)
            {
                SelectListItem selectItemForTestingOrg = new SelectListItem();
                selectItemForTestingOrg.Text = itemForTestingOrg.Name;
                selectItemForTestingOrg.Value = itemForTestingOrg.Id.ToString() + ":" + itemForTestingOrg.Name;
                if (collection["TName"].Contains(itemForTestingOrg.Name)) selectItemForTestingOrg.Selected = true;
                itemsForTestingOrg.Add(selectItemForTestingOrg);
            }
            ViewData["TestingOrg"] = itemsForTestingOrg;            
            string strId = collection["Id"].ToString();
            string strName = collection["Name"].ToString();
            string strWeight = collection["Weight"].ToString();
            string strCerNum = collection["CerNum"].ToString();
            string strBarcode = collection["Barcode"].ToString();
            string strPrice = collection["Price"].ToString();
            string strStandard = collection["Standard"].ToString();
            string strCId = collection["CId"].ToString();
            string strCName = collection["CName"].ToString();
            string strTId = collection["TId"].ToString();
            string strTName = collection["TName"].ToString();
            string strCategory = collection["Category"].ToString();
            //为钻石，黄镶宝产品准备
            string strMainStone = "";
            string strMainStoneCarats = "";
            string strMainStoneClarity = "";
            string strMainStoneColor = "";
            string strSize = "";
           
            strMainStone = collection["MainStone"].ToString();
            strMainStoneCarats = collection["MainStoneCarats"].ToString();
            strMainStoneClarity = collection["MainStoneClarity"].ToString();
            strMainStoneColor = collection["MainStoneColor"].ToString();
            strSize = collection["Size"].ToString();

            string[] arrId = strId.Split(',');
            string[] arrName = strName.Split(',');
            string[] arrWeight = strWeight.Split(',');

            string[] arrCerNum = strCerNum.Split(',');
            string[] arrBarcode = strBarcode.Split(',');

            string[] arrPrice = strPrice.Split(',');
            string[] arrStandard = strStandard.Split(',');
            string[] arrCId = strCId.Split(',');
            string[] arrCName = strCName.Split(',');
            string[] arrTId = strTId.Split(',');
            string[] arrTName = strTName.Split(',');
            string[] arrCategory = strCategory.Split(',');
            //为钻石，黄镶宝产品准备
            string[] arrMainStone = strMainStone.Split(',');
            string[] arrMainStoneCarats = strMainStoneCarats.Split(',');
            string[] arrMainStoneClarity = strMainStoneClarity.Split(',');
            string[] arrMainStoneColor = strMainStoneColor.Split(',');
            string[] arrSize = strSize.Split(',');
            //产品类别
            ViewData["Category"] = arrCategory[0];
            
            //执行标准
            ViewData["Standard"] = arrStandard[0];
            /////////////创建DataTable////////////////
            DataTable dt = new DataTable();
            //创建DataTable Columns
            DataColumn dcId = new DataColumn("Id",typeof(string));
            DataColumn dcName = new DataColumn("Name", typeof(string));
            DataColumn dcWeight = new DataColumn("Weight", typeof(string));
            DataColumn dcCerNum = new DataColumn("CerNum", typeof(string));
            DataColumn dcBarcode = new DataColumn("Barcode", typeof(string));
            DataColumn dcPrice = new DataColumn("Price", typeof(string));
            DataColumn dcStandard = new DataColumn("Standard", typeof(string));
            DataColumn dCId = new DataColumn("CId", typeof(string));
            DataColumn dcCName = new DataColumn("CName", typeof(string));
            DataColumn dcTId = new DataColumn("TId", typeof(string));
            DataColumn dcTName = new DataColumn("TName", typeof(string));
            DataColumn dcCategory = new DataColumn("Category", typeof(string));
            //为钻石和黄镶宝准备

            DataColumn dcMainStone = new DataColumn("MainStone", typeof(string));
            DataColumn dcMainStoneCarats = new DataColumn("MainStoneCarats", typeof(string));
            DataColumn dcMainStoneClarity = new DataColumn("MainStoneClarity", typeof(string));
            DataColumn dcMainStoneColor = new DataColumn("MainStoneColor", typeof(string));
            DataColumn dcSize = new DataColumn("Size", typeof(string));
            dt.Columns.Add(dcId);
            dt.Columns.Add(dcName);
            dt.Columns.Add(dcWeight);
            dt.Columns.Add(dcCerNum);
            dt.Columns.Add(dcBarcode);
            dt.Columns.Add(dcPrice);
            dt.Columns.Add(dcStandard);
            dt.Columns.Add(dCId);
            dt.Columns.Add(dcCName);
            dt.Columns.Add(dcTId);
            dt.Columns.Add(dcTName);
            dt.Columns.Add(dcCategory);
            //为钻石和黄镶宝准备
            dt.Columns.Add(dcMainStone);
            dt.Columns.Add(dcMainStoneCarats);
            dt.Columns.Add(dcMainStoneClarity);
            dt.Columns.Add(dcMainStoneColor);
            dt.Columns.Add(dcSize);

            //创建DataTable Rows
            for (int i = 0; i < arrName.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = arrId[i];
                dr["Name"] = arrName[i];
                dr["Weight"] = arrWeight[i];
                dr["CerNum"] = arrCerNum[i];
                dr["Barcode"] = arrBarcode[i];
                dr["Price"] = arrPrice[i];
                dr["Standard"] = arrStandard[i];
                dr["CId"] = arrCId[i];
                dr["CName"] = arrCName[i];
                dr["TId"] = arrTId[i];
                dr["TName"] = arrTName[i];
                dr["Category"] = arrCategory[i];
                //为钻石和黄镶宝准备
                dr["MainStone"] = arrMainStone[i];
                dr["MainStoneCarats"] = arrMainStoneCarats[i];
                dr["MainStoneClarity"] = arrMainStoneClarity[i];
                dr["MainStoneColor"] = arrMainStoneColor[i];
                dr["Size"] = arrSize[i];

                dt.Rows.Add(dr);
            }
            List<ProductModel> list = ConvertHelper<ProductModel>.DataTableToList(dt);
            return View(list);
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public ActionResult Upload(List<ProductModel> list) {
            return View(list);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        public ActionResult Upload()
          
        {
            string strFileName = "";
            string strSeverDataPath = System.Web.HttpContext.Current.Server.MapPath("~/App_Data/");
            if(!Directory.Exists(strSeverDataPath))//判断路径是否存在
            {
                Directory.CreateDirectory(strSeverDataPath);
            }
            string strSaveFilePath = "";

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
            strStandard = strStandard.Replace(',', '、');
            string strCategory = Request["Category"];
            Stream st = null;
            if (uploadfile != null && uploadfile.ContentLength > 0)
            {
                ///先保存上传的文件到server,begining
                strFileName = uploadfile.FileName;
                st = uploadfile.InputStream;
                strSaveFilePath = Path.Combine(strSeverDataPath,strFileName);
                uploadfile.SaveAs(strSaveFilePath);
                
                ///先保存上传的文件到server,ending

                ExcelHelper excelobj = new ExcelHelper(strSaveFilePath);
                try 
                {                   
                    DataTable dt = excelobj.GetDataTable("sheet1", true);

                    DataTable newdt = dt.Copy();

                    //修改列名,使Excel列名和Model属性一致
                    foreach (DataColumn dc in newdt.Columns)
                    {
                        string colName = dc.ColumnName;
                        
                        if (colName.Contains('_'))
                        {
                            string[] strTemp = colName.Split('_');
                            if (strTemp.Length > 0)
                            {
                                dc.ColumnName = strTemp[1];
                            }
                        }
                        else
                        {
                            if (strCategory.Contains("钻石"))
                            {
                                switch (colName)
                                {
                                    case "证书编号":
                                        dc.ColumnName = "CerNum";
                                        break;
                                    case "条码号":
                                        dc.ColumnName = "Barcode";
                                        break;
                                    case "品名":
                                        dc.ColumnName = "Name";
                                        break;
                                    case "重量":
                                        dc.ColumnName = "Weight";
                                        break;
                                    case "售价":
                                        dc.ColumnName = "Price";
                                        break;
                                    case "主石":
                                        dc.ColumnName = "MainStone";
                                        break;
                                    case "主石重量":
                                        dc.ColumnName = "MainStoneCarats";
                                        break;
                                    case "主石净度":
                                        dc.ColumnName = "MainStoneClarity";
                                        break;
                                    case "主石颜色":
                                        dc.ColumnName = "MainStoneColor";
                                        break;
                                    case "尺寸":
                                        dc.ColumnName = "Size";
                                        break;
                                    default:
                                        return RedirectToAction("AddProducts", "Product");
                                }
                            }
                            else if (strCategory.Contains("黄镶宝"))
                            {
                                switch (colName)
                                {
                                    case "证书编号":
                                        dc.ColumnName = "CerNum";
                                        break;
                                    case "条码号":
                                        dc.ColumnName = "Barcode";
                                        break;
                                    case "品名":
                                        dc.ColumnName = "Name";
                                        break;
                                    case "产品总重":
                                        dc.ColumnName = "Weight";
                                        break;
                                    case "售价":
                                        dc.ColumnName = "Price";
                                        break;
                                    case "产品金重":
                                        dc.ColumnName = "MainStoneCarats";
                                        break;
                                    default:
                                        return RedirectToAction("AddProducts", "Product");
                                }
                            }
                            else
                            {
                                switch (colName)
                                {
                                    case "证书编号":
                                        dc.ColumnName = "CerNum";
                                        break;
                                    case "条码号":
                                        dc.ColumnName = "Barcode";
                                        break;
                                    case "品名":
                                        dc.ColumnName = "Name";
                                        break;
                                    case "重量":
                                        dc.ColumnName = "Weight";
                                        break;
                                    case "售价":
                                        dc.ColumnName = "Price";
                                        break;
                                    default:
                                        return RedirectToAction("AddProducts", "Product");
                                }
                            }
                          
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

                    if(!strCategory.Contains("钻石")){
                        int iTennorInGold = 999;
                        if (strCategory.Contains("Au750"))
                        {
                            iTennorInGold = 750;
                        }
                        else if (strCategory.Contains("Au916"))
                        {
                            iTennorInGold = 916;
                        }
                        else if (strCategory.Contains("黄镶宝"))
                        {
                            iTennorInGold = 0;
                        }

                        DataColumn dcTenorInGold = new DataColumn("TenorInGold", typeof(int));
                        dcTenorInGold.DefaultValue = iTennorInGold;
                        newdt.Columns.Add(dcTenorInGold);
                    }
                    List<ProductModel> list = new List<ProductModel>();
                    list = ConvertHelper<ProductModel>.DataTableToList(newdt);
                    return View(list);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("",ex.Message);
                    return RedirectToAction("AddProducts","Product");
                }
                
            }
            else
            {
                return Content("Empty excel");
            }       
                
        }

        /// <summary>
        /// 若记录重复，显示待保存信息
        /// 若保存成功，显示已保存信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveProducts(List<ProductModel> model)
        {
            return View(model);
        }
        /// <summary>
        /// 批量上已读取的Excel数据到MySQL DataBase
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
          public ActionResult SaveProducts()
          {
                  string strName = Request["Name"].ToString();
                  string strWeight = Request["Weight"].ToString();
                  string strCerNum = Request["CerNum"].ToString();
                  string strBarcode = Request["Barcode"].ToString();
                  string strPrice = Request["Price"].ToString();
                  string strStandard = Request["Standard"].ToString();
                  string strCId = Request["CId"].ToString();
                  string strCName = Request["CName"].ToString();
                  string strTId = Request["TId"].ToString();
                  string strTName = Request["TName"].ToString();
                  string strCategory = Request["Category"].ToString();

                  string strMainStone = "";
                  string strMainStoneCarats = "";
                  string strMainStoneClarity = "";
                  string strMainStoneColor = "";
                  string strSize = "";
                  string strTenorInGold = "";
                  string[] arrName = strName.Split(',');
                  string[] arrWeight = strWeight.Split(',');

                  string[] arrCerNum = strCerNum.Split(',');
                  string[] arrBarcode = strBarcode.Split(',');

                  string[] arrPrice = strPrice.Split(',');
                  string[] arrStandard = strStandard.Split(',');
                  string[] arrCId = strCId.Split(',');
                  string[] arrCName = strCName.Split(',');
                  string[] arrTId = strTId.Split(',');
                  string[] arrTName = strTName.Split(',');
                  string[] arrCategory = strCategory.Split(',');
                  if (strCategory.Contains("钻石"))
                  {
                      strMainStone = Request["MainStone"];
                      strMainStoneCarats = Request["MainStoneCarats"];
                      strMainStoneClarity = Request["MainStoneClarity"];
                      strMainStoneColor = Request["MainStoneColor"];
                      strSize = Request["Size"];
                  }
                 else if (strCategory.Contains("黄镶宝"))
                  {
                      strMainStoneCarats = Request["MainStoneCarats"];
                  } 
                  else
                  {
                      strTenorInGold = Request["TenorInGold"];
                  }
                  string[] arrMainStone = strMainStone.Split(',');
                  string[] arrMainStoneCarats = strMainStoneCarats.Split(',');
                  string[] arrMainStoneClarity = strMainStoneClarity.Split(',');
                  string[] arrMainStoneColor = strMainStoneColor.Split(',');
                  string[] arrSize = strSize.Split(',');
                  string[] arrTenorInGold = strTenorInGold.Split(',');
                  //创建DataTable
                  DataTable dt = new DataTable();
                  DataColumn dcName = new DataColumn("Name", typeof(string));
                  DataColumn dcWeight = new DataColumn("Weight", typeof(string));
                  DataColumn dcCerNum = new DataColumn("CerNum", typeof(string));
                  DataColumn dcBarcode = new DataColumn("Barcode", typeof(string));
                  DataColumn dcPrice = new DataColumn("Price", typeof(string));
                  DataColumn dcStandard = new DataColumn("Standard", typeof(string));
                  DataColumn dCId = new DataColumn("CId", typeof(string));
                  DataColumn dcCName = new DataColumn("CName", typeof(string));
                  DataColumn dcTId = new DataColumn("TId", typeof(string));
                  DataColumn dcTName = new DataColumn("TName", typeof(string));
                  DataColumn dcCategory = new DataColumn("Category", typeof(string));

                  DataColumn dcExist = new DataColumn("Exist", typeof(Int32));
                  dt.Columns.Add(dcName);
                  dt.Columns.Add(dcWeight);
                  dt.Columns.Add(dcCerNum);
                  dt.Columns.Add(dcBarcode);
                  dt.Columns.Add(dcPrice);
                  dt.Columns.Add(dcStandard);
                  dt.Columns.Add(dCId);
                  dt.Columns.Add(dcCName);
                  dt.Columns.Add(dcTId);
                  dt.Columns.Add(dcTName);
                  dt.Columns.Add(dcCategory);
                  if (strCategory.Contains("钻石"))
                  {
                      DataColumn dcMainStone = new DataColumn("MainStone", typeof(string));
                      dt.Columns.Add(dcMainStone);
                      DataColumn dcMainStoneCarats = new DataColumn("MainStoneCarats", typeof(string));
                      dt.Columns.Add(dcMainStoneCarats);
                      DataColumn dcMainStoneClarity = new DataColumn("MainStoneClarity", typeof(string));
                      dt.Columns.Add(dcMainStoneClarity);
                      DataColumn dcMainStoneColor = new DataColumn("MainStoneColor", typeof(string));
                      dt.Columns.Add(dcMainStoneColor);
                      DataColumn dcSize = new DataColumn("Size", typeof(string));
                      dt.Columns.Add(dcSize);
                  }
                  else if (strCategory.Contains("黄镶宝"))
                  {
                      DataColumn dcMainStoneCarats = new DataColumn("MainStoneCarats", typeof(string));
                      dt.Columns.Add(dcMainStoneCarats);
                  }
                  else
                  {
                      DataColumn dcTenorInGold = new DataColumn("TenorInGold", typeof(string));
                      dt.Columns.Add(dcTenorInGold);
                  }
                  dt.Columns.Add(dcExist);
                  //创建已存在记录表
                  // DataTable dtExist = dt.Clone();
                  for (int i = 0; i < arrName.Length; i++)
                  {
                      DataRow dr = dt.NewRow();

                      dr["Name"] = arrName[i] ?? "";
                      dr["Weight"] = arrWeight[i] ?? "";
                      dr["CerNum"] = arrCerNum[i] ?? "";
                      dr["Barcode"] = arrBarcode[i] ?? "";
                      dr["Price"] = arrPrice[i] ?? "";
                      dr["Standard"] = arrStandard[i] ?? "";
                      dr["CId"] = arrCId[i] ?? "";
                      dr["CName"] = arrCName[i] ?? "";
                      dr["TId"] = arrTId[i] ?? "";
                      dr["TName"] = arrTName[i] ?? "";
                      dr["Category"] = arrCategory[i] ?? "";
                      if (arrCategory[i].Contains("钻石"))
                      {
                          dr["MainStone"] = arrMainStone[i] ?? "";
                          dr["MainStoneCarats"] = arrMainStoneCarats[i] ?? "";
                          dr["MainStoneClarity"] = arrMainStoneClarity[i] ?? "";
                          dr["MainStoneColor"] = arrMainStoneColor[i] ?? "";
                          dr["Size"] = arrSize[i] ?? "";
                      }
                      else if (strCategory.Contains("黄镶宝"))//产品金重
                      {
                          dr["MainStoneCarats"] = arrMainStoneCarats[i] ?? "";
                      }
                      else
                      {
                          dr["TenorInGold"] = arrTenorInGold[i] ?? "";
                      }
                      dr["Exist"] = 0;
                      dt.Rows.Add(dr);
                      //判断数据是否已存在
                      string mysql_sel = "select * from p_info_table where P_CerNum=";
                      mysql_sel += "'" + arrCerNum[i] + "'";
                      DataTable tempdt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, mysql_sel, null);
                      if (tempdt.Rows.Count > 0)
                      {
                          dt.Rows[i]["Exist"] = 1;
                      }
                      //若品名，条码号，证书编号为空，则不能保存
                      if (dr["Name"].ToString().Length < 1 || dr["Barcode"].ToString().Length < 1 || dr["CerNum"].ToString().Length < 1)
                      {
                          dt.Rows[i]["Exist"] = 1;
                      }
                  }
                  if (dt.Rows.Count > 0 && dt.Select("Exist=1").Length < 1)
                  {
                      string strCommandText = "";

                      List<MySqlParameter> paramList = new List<MySqlParameter>();
                      paramList.Add(new MySqlParameter("@Name", MySqlDbType.VarChar, 100, "Name"));
                      paramList.Add(new MySqlParameter("@Weight", MySqlDbType.Float, 100, "Weight"));
                      paramList.Add(new MySqlParameter("@CerNum", MySqlDbType.VarChar, 100, "CerNum"));
                      paramList.Add(new MySqlParameter("@Barcode", MySqlDbType.VarChar, 100, "Barcode"));
                      paramList.Add(new MySqlParameter("@Price", MySqlDbType.Int32, 100, "Price"));
                      paramList.Add(new MySqlParameter("@Standard", MySqlDbType.VarChar, 100, "Standard"));
                      paramList.Add(new MySqlParameter("@Category", MySqlDbType.VarChar, 100, "Category"));
                      paramList.Add(new MySqlParameter("@CId", MySqlDbType.Int32, 100, "CId"));
                      paramList.Add(new MySqlParameter("@TId", MySqlDbType.Int32, 100, "TId"));
                      if (strCategory.Contains("钻石"))
                      {
                          strCommandText = "INSERT INTO p_info_table (P_Name,P_Weight,P_CerNum,P_Barcode,P_Price,P_Standard,P_Category,P_CId,P_Tid,P_MainStone,P_MainStoneCarats,P_MainStoneClarity,P_MainStoneColor,P_Size) VALUES(@Name,@Weight,@CerNum,@Barcode,@Price,@Standard,@Category,@CId,@TId,@MainStone,@MainStoneCarats,@MainStoneClarity,@MainStoneColor,@Size)";
                          paramList.Add(new MySqlParameter("@MainStone", MySqlDbType.VarChar, 100, "MainStone"));
                          paramList.Add(new MySqlParameter("@MainStoneCarats", MySqlDbType.Float, 100, "MainStoneCarats"));
                          paramList.Add(new MySqlParameter("@MainStoneClarity", MySqlDbType.VarChar, 100, "MainStoneClarity"));
                          paramList.Add(new MySqlParameter("@MainStoneColor", MySqlDbType.VarChar, 100, "MainStoneColor"));
                          paramList.Add(new MySqlParameter("@Size", MySqlDbType.Int32, 100, "Size"));
                      }
                      else if (strCategory.Contains("黄镶宝"))//产品金重
                      {
                          strCommandText = "INSERT INTO p_info_table (P_Name,P_Weight,P_CerNum,P_Barcode,P_Price,P_Standard,P_Category,P_CId,P_Tid,P_MainStoneCarats) VALUES(@Name,@Weight,@CerNum,@Barcode,@Price,@Standard,@Category,@CId,@TId,@MainStoneCarats)";
                          paramList.Add(new MySqlParameter("@MainStoneCarats", MySqlDbType.Float, 100, "MainStoneCarats"));    
                      }
                      else if (strCategory.Contains("硬金"))//Remarks：工艺
                      {
                          strCommandText = "INSERT INTO p_info_table (P_Name,P_Weight,P_CerNum,P_Barcode,P_Price,P_Standard,P_Category,P_CId,P_Tid,P_TenorInGold,P_Remarks) VALUES(@Name,@Weight,@CerNum,@Barcode,@Price,@Standard,@Category,@CId,@TId,@TenorInGold,'3D工艺')";
                          paramList.Add(new MySqlParameter("@TenorInGold", MySqlDbType.Int32, 100, "TenorInGold"));
                      }
                      else
                      {
                          strCommandText = "INSERT INTO p_info_table (P_Name,P_Weight,P_CerNum,P_Barcode,P_Price,P_Standard,P_Category,P_CId,P_Tid,P_TenorInGold) VALUES(@Name,@Weight,@CerNum,@Barcode,@Price,@Standard,@Category,@CId,@TId,@TenorInGold)";
                          paramList.Add(new MySqlParameter("@TenorInGold", MySqlDbType.Int32, 100, "TenorInGold"));
                      }

                      MySqlParameter[] commadparameters = paramList.ToArray();
                      //插入数据库
                      try
                      {
                          bool da = MySQLHelper.ExecuteDataAdapterBatch(MySQLHelper.Conn, CommandType.Text, strCommandText, dt, 5000, commadparameters);
                          if (da)
                          {
                              //创建删除日志
                              //LogModel logModel = new LogModel();
                              //logModel.Name = User.Identity.Name;
                              //logModel.Date = DateTime.Now;
                              //logModel.Content = "AddProducts";
                              //try
                              //{
                              //    logModel.AddLog(logModel);
                              //}
                              //catch (Exception ex)
                              //{
                              //    ModelState.AddModelError("", "日志创建失败");
                              //}
                              List<ProductViewModel> list_update = ConvertHelper<ProductViewModel>.DataTableToList(dt);
                              ProductSuccessViewModel vm = new ProductSuccessViewModel();
                              vm.ProductList = list_update;
                              vm.UploadDateTime = DateTime.Now.ToString();
                              vm.UploadNum = dt.Rows.Count;
                              vm.UploadUserName = User.Identity.Name;
                              return View(vm);
                          }
                          else
                          {
                              throw new Exception("数据记录没有保存成功");
                          }
                      }
                      catch (Exception ex)
                      {
                          Session["errMsg"] = ex.Message;
                          return RedirectToAction("AddProducts", "Product");

                      }

                      //检查记录重复，回到待保存页面
                  }
                  else
                  {
                      List<ProductViewModel> list = ConvertHelper<ProductViewModel>.DataTableToList(dt);
                      ProductSuccessViewModel vm2 = new ProductSuccessViewModel();
                      vm2.ProductList = list;
                      vm2.UploadDateTime = "";
                      vm2.UploadNum = 0;
                      vm2.UploadUserName = User.Identity.Name;
                      return View(vm2);
                  }
         }  

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult SaveProductsForUpdate(List<ProductModel> model)
        {
            return View(model);
        }
        /// <summary>
        /// 保存修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveProductsForUpdate()
        {
            string strId = Request["Id"].ToString();
            string strName = Request["Name"].ToString();
            string strWeight = Request["Weight"].ToString();
            string strCerNum = Request["CerNum"].ToString();
            string strBarcode = Request["Barcode"].ToString();
            string strPrice = Request["Price"].ToString();
            string strStandard = Request["Standard"].ToString();

            string strCompany = Request["Company"];
            string[] arrCompany = strCompany.Split(':');
            string strCId = arrCompany[0];
            string strCName = arrCompany[1];

            string strTestingOrg = Request["TestingOrg"];
            string[] arrTestingOrg = strTestingOrg.Split(':');
            string strTId = arrTestingOrg[0];
            string strTName = arrTestingOrg[1];

            string strCategory = Request["Category"].ToString();

            string strMainStone = "";
            string strMainStoneCarats = "";
            string strMainStoneClarity = "";
            string strMainStoneColor = "";
            string strSize = "";
            string strTenorInGold = "";
            if (strCategory.Contains("钻石"))
            {
                strMainStone = Request["MainStone"];
                strMainStoneCarats = Request["MainStoneCarats"];
                strMainStoneClarity = Request["MainStoneClarity"];
                strMainStoneColor = Request["MainStoneColor"];
                strSize = Request["Size"];
            }
            else if (strCategory.Contains("黄镶宝"))
            {
                strMainStoneCarats = Request["MainStoneCarats"];
            }
            else
            {
                strTenorInGold = Request["TenorInGold"];
            }
            string[] arrId = strId.Split(',');
            string[] arrName = strName.Split(',');
            string[] arrWeight = strWeight.Split(',');

            string[] arrCerNum = strCerNum.Split(',');
            string[] arrBarcode = strBarcode.Split(',');

            string[] arrPrice = strPrice.Split(',');

            string[] arrMainStone = strMainStone.Split(',');
            string[] arrMainStoneCarats = strMainStoneCarats.Split(',');
            string[] arrMainStoneClarity = strMainStoneClarity.Split(',');
            string[] arrMainStoneColor = strMainStoneColor.Split(',');
            string[] arrSize = strSize.Split(',');
            //创建DataTable
            DataTable dt = new DataTable();
            DataColumn dcId = new DataColumn("Id", typeof(string));
            DataColumn dcName = new DataColumn("Name", typeof(string));
            DataColumn dcWeight = new DataColumn("Weight", typeof(string));
            DataColumn dcCerNum = new DataColumn("CerNum", typeof(string));
            DataColumn dcBarcode = new DataColumn("Barcode", typeof(string));
            DataColumn dcPrice = new DataColumn("Price", typeof(string));
            DataColumn dcStandard = new DataColumn("Standard", typeof(string));
            DataColumn dCId = new DataColumn("CId", typeof(string));
            DataColumn dcCName = new DataColumn("CName", typeof(string));
            DataColumn dcTId = new DataColumn("TId", typeof(string));
            DataColumn dcTName = new DataColumn("TName", typeof(string));
            DataColumn dcCategory = new DataColumn("Category", typeof(string));
            if (!strCategory.Contains("钻石"))
            {
                int iTennorInGold = 999;
                if (strCategory.Contains("Au750"))
                {
                    iTennorInGold = 750;
                }
                else if (strCategory.Contains("Au916"))
                {
                    iTennorInGold = 916;
                }
                else if (strCategory.Contains("黄镶宝"))
                {
                    iTennorInGold = 0;
                }

                DataColumn dcTenorInGold = new DataColumn("TenorInGold", typeof(int));
                dcTenorInGold.DefaultValue = iTennorInGold;
                dt.Columns.Add(dcTenorInGold);
            }
            DataColumn dcExist = new DataColumn("Exist", typeof(Int32));

            dt.Columns.Add(dcId);
            dt.Columns.Add(dcName);
            dt.Columns.Add(dcWeight);
            dt.Columns.Add(dcCerNum);
            dt.Columns.Add(dcBarcode);
            dt.Columns.Add(dcPrice);
            dt.Columns.Add(dcStandard);
            dt.Columns.Add(dCId);
            dt.Columns.Add(dcCName);
            dt.Columns.Add(dcTId);
            dt.Columns.Add(dcTName);
            dt.Columns.Add(dcCategory);
            if (strCategory.Contains("钻石"))
            {
                DataColumn dcMainStone = new DataColumn("MainStone", typeof(string));
                dt.Columns.Add(dcMainStone);
                DataColumn dcMainStoneCarats = new DataColumn("MainStoneCarats", typeof(string));
                dt.Columns.Add(dcMainStoneCarats);
                DataColumn dcMainStoneClarity = new DataColumn("MainStoneClarity", typeof(string));
                dt.Columns.Add(dcMainStoneClarity);
                DataColumn dcMainStoneColor = new DataColumn("MainStoneColor", typeof(string));
                dt.Columns.Add(dcMainStoneColor);
                DataColumn dcSize = new DataColumn("Size", typeof(string));
                dt.Columns.Add(dcSize);
            }
            else if (strCategory.Contains("黄镶宝"))
            {
                DataColumn dcMainStoneCarats = new DataColumn("MainStoneCarats", typeof(string));
                dt.Columns.Add(dcMainStoneCarats);
            }
            dt.Columns.Add(dcExist);
            //创建已存在记录表
            // DataTable dtExist = dt.Clone();
            for (int i = 0; i < arrName.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dr["Id"] = arrId[i] ?? "";
                dr["Name"] = arrName[i] ?? "";
                dr["Weight"] = arrWeight[i] ?? "";
                dr["CerNum"] = arrCerNum[i] ?? "";
                dr["Barcode"] = arrBarcode[i] ?? "";
                dr["Price"] = arrPrice[i] ?? "";
                dr["Standard"] = strStandard ?? "";
                dr["CId"] = strCId ?? "";
                dr["CName"] = strCName ?? "";
                dr["TId"] = strTId ?? "";
                dr["TName"] = strTName ?? "";
                dr["Category"] = strCategory ?? "";
                if (strCategory.Contains("钻石"))
                {
                    dr["MainStone"] = arrMainStone[i] ?? "";
                    dr["MainStoneCarats"] = arrMainStoneCarats[i] ?? "0";
                    dr["MainStoneClarity"] = arrMainStoneClarity[i] ?? "";
                    dr["MainStoneColor"] = arrMainStoneColor[i] ?? "";
                    dr["Size"] = arrSize[i] ?? "0";
                }
                else if (strCategory.Contains("黄镶宝"))
                {
                    dr["MainStoneCarats"] = arrMainStoneCarats[i] ?? "0";
                }
                dr["Exist"] = 0;
                dt.Rows.Add(dr);
                /*************************判断数据是否已存在********************************************/
                //检查数据库中是否有已经存在的纪录
                string mysql_sel = "select * from p_info_table where P_CerNum=";
                mysql_sel += "'" + arrCerNum[i] + "'";
                mysql_sel += "and not P_Id='" + arrId[i] + "'";
                DataTable tempdt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, mysql_sel, null);
                if (tempdt.Rows.Count > 0)
                {
                    dt.Rows[i]["Exist"] = 1;
                }
                //若品名，条码号，证书编号为空，则不能保存
                if (dr["Name"].ToString().Length < 1 || dr["Barcode"].ToString().Length < 1 || dr["CerNum"].ToString().Length < 1)
                {
                    dt.Rows[i]["Exist"] = 1;
                }
                //当产品为"黄镶宝"时，验证产品总重和产品金重的重量是否正确
                if (strCategory.Contains("黄镶宝"))
                {
                    try
                    {
                        if (Single.Parse(arrWeight[i]) < Single.Parse(arrMainStoneCarats[i]))
                        {
                            dt.Rows[i]["Exist"] = -1;
                        }
                    }
                    catch (Exception ex)
                    {
                        Session["errMsg"] = ex.Message;
                        return RedirectToAction("GetProducts", "Product");
                    }
                }
                
            }


            if (dt.Rows.Count > 0 && dt.Select("Exist=1").Length < 1 && dt.Rows.Count > 0 && dt.Select("Exist=-1").Length < 1)
            {
                string strCommandText = "";

                List<MySqlParameter> paramList = new List<MySqlParameter>();
                paramList.Add(new MySqlParameter("@Name", MySqlDbType.VarChar, 100, "Name"));
                paramList.Add(new MySqlParameter("@Weight", MySqlDbType.Float, 100, "Weight"));
                paramList.Add(new MySqlParameter("@CerNum", MySqlDbType.VarChar, 100, "CerNum"));
                paramList.Add(new MySqlParameter("@Barcode", MySqlDbType.VarChar, 100, "Barcode"));
                paramList.Add(new MySqlParameter("@Price", MySqlDbType.Int32, 100, "Price"));
                paramList.Add(new MySqlParameter("@Standard", MySqlDbType.VarChar, 100, "Standard"));
                paramList.Add(new MySqlParameter("@Category", MySqlDbType.VarChar, 100, "Category"));
                paramList.Add(new MySqlParameter("@CId", MySqlDbType.Int32, 100, "CId"));
                paramList.Add(new MySqlParameter("@TId", MySqlDbType.Int32, 100, "TId"));
                if (strCategory.Contains("钻石"))
                {
                    strCommandText = "UPDATE p_info_table SET P_Name=@Name,P_Weight=@Weight,P_CerNum=@CerNum,P_Barcode=@Barcode,P_Price=@Price,P_Standard=@Standard,P_Category=@Category,P_CId=@CId,P_Tid=@TId,P_MainStone=@MainStone,P_MainStoneCarats=@MainStoneCarats,P_MainStoneClarity=@MainStoneClarity,P_MainStoneColor=@MainStoneColor,P_Size=@Size  WHERE P_Id=@Id";
                    paramList.Add(new MySqlParameter("@MainStone", MySqlDbType.VarChar, 100, "MainStone"));
                    paramList.Add(new MySqlParameter("@MainStoneCarats", MySqlDbType.Float, 100, "MainStoneCarats"));
                    paramList.Add(new MySqlParameter("@MainStoneClarity", MySqlDbType.VarChar, 100, "MainStoneClarity"));
                    paramList.Add(new MySqlParameter("@MainStoneColor", MySqlDbType.VarChar, 100, "MainStoneColor"));
                    paramList.Add(new MySqlParameter("@Size", MySqlDbType.Int32, 100, "Size"));
                }
                else if (strCategory.Contains("黄镶宝"))
                {
                    strCommandText = "UPDATE p_info_table SET P_Name=@Name,P_Weight=@Weight,P_CerNum=@CerNum,P_Barcode=@Barcode,P_Price=@Price,P_Standard=@Standard,P_Category=@Category,P_CId=@CId,P_Tid=@TId,P_MainStoneCarats=@MainStoneCarats  WHERE P_Id=@Id";
                    paramList.Add(new MySqlParameter("@MainStoneCarats", MySqlDbType.Float, 100, "MainStoneCarats"));
                }
                else if (strCategory.Contains("硬金"))
                {
                    strCommandText = "UPDATE p_info_table SET P_Name=@Name,P_Weight=@Weight,P_CerNum=@CerNum,P_Barcode=@Barcode,P_Price=@Price,P_Standard=@Standard,P_Category=@Category,P_CId=@CId,P_Tid=@TId,P_TenorInGold=@TenorInGold,P_Remarks='3D工艺' WHERE P_Id=@Id";
                    paramList.Add(new MySqlParameter("@TenorInGold", MySqlDbType.Int32, 100, "TenorInGold"));
                }
                else
                {
                    strCommandText = "UPDATE p_info_table SET P_Name=@Name,P_Weight=@Weight,P_CerNum=@CerNum,P_Barcode=@Barcode,P_Price=@Price,P_Standard=@Standard,P_Category=@Category,P_CId=@CId,P_Tid=@TId,P_TenorInGold=@TenorInGold WHERE P_Id=@Id";
                    paramList.Add(new MySqlParameter("@TenorInGold", MySqlDbType.Int32, 100, "TenorInGold"));
                }
                paramList.Add(new MySqlParameter("@Id", MySqlDbType.Int32, 100, "Id"));
                MySqlParameter[] commadparameters = paramList.ToArray();
                //更新数据库
                try
                {
                    bool da = MySQLHelper.ExecuteDataAdapterBatch(MySQLHelper.Conn, CommandType.Text, strCommandText, dt, 5000, commadparameters);
                    if (da)
                    {
                        //创建删除日志
                        LogModel logModel = new LogModel();
                        logModel.Name = User.Identity.Name;
                        logModel.Date = DateTime.Now;
                        logModel.Content = "UpdateProducts";
                        try
                        {
                            logModel.AddLog(logModel);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "日志创建失败");
                        }
                        List<ProductViewModel> list_update = ConvertHelper<ProductViewModel>.DataTableToList(dt);
                        ProductSuccessViewModel vm = new ProductSuccessViewModel();
                        vm.ProductList = list_update;
                        vm.UploadDateTime = DateTime.Now.ToString();
                        vm.UploadNum = dt.Rows.Count;
                        vm.UploadUserName = User.Identity.Name;                      
                        return View(vm);
                    }
                    else
                    {
                        throw new Exception("数据记录没有保存成功");
                    }
                }
                catch (Exception ex)
                {
                    Session["errMsg"] = ex.Message;
                    return RedirectToAction("AddProducts", "Product");

                }


            }
            else //检查记录重复，回到待保存页面
            {
                List<ProductViewModel> list = ConvertHelper<ProductViewModel>.DataTableToList(dt);
                ProductSuccessViewModel vm2 = new ProductSuccessViewModel();
                vm2.ProductList = list;
                vm2.UploadDateTime = "";
                vm2.UploadNum = 0;
                vm2.UploadUserName = User.Identity.Name;
                vm2.Category = strCategory;
                vm2.Standard = strStandard;

                //公司列表
                CompanyModel modelForCompany = new CompanyModel();
                List<SelectListItem> itemsForCompany = new List<SelectListItem>();
                List<CompanyModel> clist = modelForCompany.GetAllCompanys();
                //增加“所有公司”到公司信息列表
                SelectListItem selectItemAForCompany = new SelectListItem();
                foreach (CompanyModel citem in clist)
                {
                    SelectListItem selectItemForCompany = new SelectListItem();
                    selectItemForCompany.Text = citem.Name;
                    selectItemForCompany.Value = citem.Id.ToString() + ":" + citem.Name;
                    if (strCompany.Contains(citem.Name)) selectItemForCompany.Selected = true;
                    itemsForCompany.Add(selectItemForCompany);
                }
                vm2.Company = itemsForCompany;
                //检测机构列表
                TestingOrgModel modelForTestingOrg = new TestingOrgModel();
                List<SelectListItem> itemsForTestingOrg = new List<SelectListItem>();
                List<TestingOrgModel> listForTestingOrg = modelForTestingOrg.GetAllTestingOrgs();
                foreach (TestingOrgModel itemForTestingOrg in listForTestingOrg)
                {
                    SelectListItem selectItemForTestingOrg = new SelectListItem();
                    selectItemForTestingOrg.Text = itemForTestingOrg.Name;
                    selectItemForTestingOrg.Value = itemForTestingOrg.Id.ToString() + ":" + itemForTestingOrg.Name;
                    if (strTestingOrg.Contains(itemForTestingOrg.Name)) selectItemForTestingOrg.Selected = true;
                    itemsForTestingOrg.Add(selectItemForTestingOrg);
                }
                vm2.TestingOrg = itemsForTestingOrg;
                return View(vm2);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteProducts()
        {
            if (User.Identity.Name.ToLower() != "admin")
            {
                return Content("您不是管理员，没有此权限");
            }
            else{ 
                string strId = Request["Id"].ToString();
                string[] arrId = strId.Split(',');
                //创建DataTable
                DataTable dt = new DataTable();
                DataColumn dcId = new DataColumn("Id", typeof(string));
                dt.Columns.Add(dcId);
                for (int i = 0; i < arrId.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = arrId[i] ?? "";
                    dt.Rows.Add(dr);
                }
                string strCommandText = "UPDATE p_info_table SET P_Status='X' WHERE P_Id =@Id";
                List<MySqlParameter> paramList = new List<MySqlParameter>();
                paramList.Add(new MySqlParameter("@Id", MySqlDbType.Int32, 100, "Id"));
                MySqlParameter[] commadparameters = paramList.ToArray();
                //更新数据库
                try
                {
                    bool da = MySQLHelper.ExecuteDataAdapterBatch(MySQLHelper.Conn, CommandType.Text, strCommandText, dt, 5000, commadparameters);
                    if (da)
                    {
                        //创建删除日志
                        LogModel logModel = new LogModel();
                        logModel.Name = User.Identity.Name;
                        logModel.Date = DateTime.Now;
                        logModel.Content = "DeleteProducts";
                        try
                        {
                            logModel.AddLog(logModel);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", "日志创建失败");
                        }

                        return RedirectToAction("GetProducts", "Product");
                    }
                    else
                    {
                        throw new Exception("数据记录没有删除成功");
                    }
                
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
