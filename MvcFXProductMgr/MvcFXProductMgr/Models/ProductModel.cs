using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcFXProductMgr.Models
{
    public class ProductModel
    {
        private int iNumberOfEntries = 1;
        [Key]
        public int Id { set; get; }
        public string Barcode { set; get; }
        [Key]
        public string CerNum { set; get; }
        public string Name { set; get; }
        public float Weight { set; get; }
        public int Price { set; get; }
        public string Standard { set; get; }
        public string Category { set; get; }

        public int CId { set; get; }
        public string CName { set; get; }
        public string CAddress { set; get; }
        public string CUrl { set; get; }
        public string CTel { set; get; }

        public int TId { set; get; }
        public string TName { set; get; }
        public string TUrl { set; get; }
        public string TTel { set; get; }

        public int AId { set; get; }
        public string AName { set; get; }
        public string AUrl { set; get; }
        public string Remarks { set; get; }
        public string Status { set; get; }
        
        //Get:
        public ProductModel GetProduct(string cer_num,string name)
        {
            string strCommandText = "SELECT P_Id as Id,P_Name as Name ,P_Barcode as Barcode,P_CerNum as CerNum,P_Weight as Weight,P_Price as Price,P_Standard as Standard,P_Category as Category,P_CId as CId ,C_Name AS CName,C_Address as CAddress,C_Url as CUrl,C_Tel as CTel,P_TId as TId,T_Name AS TName,T_Url as TUrl,T_Tel as TTel,P_Date as Date FROM p_info_table,t_info_table,c_info_table WHERE P_CId=C_Id AND P_TId=T_Id ";
            strCommandText += "AND P_CerNum='" + cer_num + "'AND P_Name='" + name + "'";
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, System.Data.CommandType.Text, strCommandText, null);
                ProductModel model = new ProductModel();
                if (dt.Rows.Count == 1)
                {
                    model.Id = Int32.Parse(dt.Rows[0]["Id"].ToString());
                    model.Name = dt.Rows[0]["Name"].ToString();
                    model.Barcode = dt.Rows[0]["Barcode"].ToString();
                    model.CerNum = dt.Rows[0]["CerNum"].ToString();
                    model.Weight = Single.Parse(dt.Rows[0]["Weight"].ToString());
                    model.Price = Int32.Parse(dt.Rows[0]["Price"].ToString());
                    model.Standard = dt.Rows[0]["Standard"].ToString();
                    model.CId = Int32.Parse(dt.Rows[0]["CId"].ToString());
                    model.CName = dt.Rows[0]["CName"].ToString();
                    model.CAddress = dt.Rows[0]["CAddress"].ToString();
                    model.CTel = dt.Rows[0]["CTel"].ToString();
                    model.CUrl = dt.Rows[0]["CUrl"].ToString();
                    model.TId = Int32.Parse(dt.Rows[0]["TId"].ToString());
                    model.TName = dt.Rows[0]["TName"].ToString();
                    model.TTel = dt.Rows[0]["TTel"].ToString();
                    model.TUrl = dt.Rows[0]["TUrl"].ToString();
                }
                else {
                   
                }
                return model;    
            }
            catch(Exception ex){
                HttpContext.Current.Response.Write(ex.Message.ToString());
                return new ProductModel();
            }
        }

        public ProductModel AddProduct(ProductModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = iNumberOfEntries + 1;
            //to do
            return item;
        }

        //Update
        public bool UpdateProduct(ProductModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //to do

            return true;
        }

        //Delete a company
        public bool DeleteProduct(int id)
        {
            //to do
            return true;
        }
        public ProductModel GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }
    }
}