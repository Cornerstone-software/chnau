using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
namespace MvcFXProductMgr.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public string Url { set; get; }
        public string Tel { set; get; }
        public string Status { set; get; }

        /// <summary>
        /// 获取指定Id的公司信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns>CompanyModel</returns>
        public CompanyModel GetCompanyById(int id)
        {
            string strCommandText = "SELECT C_Id AS CId,C_Name AS CName,C_Address AS CAddress,C_Url AS CUrl,C_Tel AS CTel FROM c_info_table WHERE C_Status='N'";
            strCommandText += " And C_Id='" + id.ToString() + "'";
            CompanyModel objCompany = new CompanyModel();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    objCompany.Id = id;
                    objCompany.Name=dt.Rows[0]["CName"].ToString();
                    objCompany.Address = dt.Rows[0]["CAddress"].ToString();
                    objCompany.Url = dt.Rows[0]["CUrl"].ToString();
                    objCompany.Tel = dt.Rows[0]["CTel"].ToString();
                }
                return objCompany;
            }
            catch(Exception ex){

                throw new Exception(ex.Message);

            }
        }
        public CompanyModel GetCompanyByName(string name)
        {
            string strCommandText = "SELECT C_Id AS CId,C_Name AS CName,C_Address AS CAddress,C_Url AS CUrl,C_Tel AS CTel FROM c_info_table WHERE C_Status='N'";
            strCommandText += " And C_Name='" + name + "'";
            CompanyModel objCompany = new CompanyModel();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    objCompany.Id = Int32.Parse(dt.Rows[0]["CId"].ToString());
                    objCompany.Name = dt.Rows[0]["CName"].ToString();
                    objCompany.Address = dt.Rows[0]["CAddress"].ToString();
                    objCompany.Url = dt.Rows[0]["CUrl"].ToString();
                    objCompany.Tel = dt.Rows[0]["CTel"].ToString();
                }
                return objCompany;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        /// <summary>
        /// 获取所有的公司信息
        /// </summary>
        /// <returns>List<CompanyModel></returns>
        public List<CompanyModel> GetAllCompanys()
        {
            string strCommandText = "SELECT C_Id AS CId,C_Name AS CName,C_Address AS CAddress,C_Url AS CUrl,C_Tel AS CTel FROM c_info_table WHERE C_Status in ('N','A')";
            List<CompanyModel> list =new List<CompanyModel>();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CompanyModel objCompany = new CompanyModel();
                        objCompany.Id = Int32.Parse(dt.Rows[i]["CId"].ToString());
                        objCompany.Name = dt.Rows[i]["CName"].ToString();
                        objCompany.Address = dt.Rows[i]["CAddress"].ToString();
                        objCompany.Url = dt.Rows[i]["CUrl"].ToString();
                        objCompany.Tel = dt.Rows[i]["CTel"].ToString();
                        list.Add(objCompany);
                    }
                }
                else
                {
                    CompanyModel objCompany = new CompanyModel();
                    objCompany.Id = Int32.Parse("1");
                    objCompany.Name = "福鑫珠宝城";
                    list.Add(objCompany);
                }
                return list;
            }
            catch(Exception ex){
                HttpContext.Current.Response.Write(ex.Message.ToString());
                CompanyModel objCompany = new CompanyModel();
                objCompany.Id = Int32.Parse("1");
                objCompany.Name = "福鑫珠宝城";
                list.Add(objCompany);
                return list;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddCompany(CompanyModel item){
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

           string strCommandText="INSERT INTO c_info_table (C_Name,C_Address,C_Url,C_Tel) VALUES(";
           strCommandText+="\'"+item.Name+"\',";
           strCommandText+="\'"+item.Address+"\',";
           strCommandText+="\'"+item.Url+"\',";
           strCommandText+="\'"+item.Tel+"\'";
           strCommandText+=");";
            try{
                int iResult=MySQLHelper.ExecuteNonQuery(MySQLHelper.Conn,CommandType.Text,strCommandText,null);
                return true;
            }catch(Exception ex){
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool UpdateCompany(CompanyModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //UPDATE c_info_table SET C_Status="Y" WHERE C_Id=2;
            string strCommandText = "UPDATE c_info_table SET";
            strCommandText += " C_Name ='"+item.Name+"',";
            strCommandText += " C_Address='"+item.Address+" ',";
            strCommandText += " C_Url='"+item.Url+"',";
            strCommandText += " C_Tel='" + item.Tel + "'";
            strCommandText += " WHERE C_Id="+item.Id;
            try
            {
                int iResult = MySQLHelper.ExecuteNonQuery(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete a company
        public void DeleteCompany(int id)
        {
            string strCommandText = "UPDATE c_info_table SET C_Status='X' WHERE";
            strCommandText += " C_Id=" + id;
            try
            {
                int iResult = MySQLHelper.ExecuteNonQuery(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}