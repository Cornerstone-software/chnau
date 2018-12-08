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
        private int iNumberOfEntries = 1;
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public string Url { set; get; }
        public int Tel { set; get; }
        public int Status { set; get; }

        /// <summary>
        /// 获取指定Id的公司信息
        /// </summary>
        /// <param name="id">公司Id</param>
        /// <returns>CompanyModel</returns>
        public CompanyModel GetCompany(int id)
        {
            return new CompanyModel();
        }
        /// <summary>
        /// 获取所有的公司信息
        /// </summary>
        /// <returns>List<CompanyModel></returns>
        public List<CompanyModel> GetAllCompanys()
        {
            string strCommandText = "SELECT C_Id AS CId,C_Name AS CName FROM c_info_table WHERE C_Status='N'";
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
        public CompanyModel AddCompany(CompanyModel item){
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = iNumberOfEntries + 1;
            //to do
            return item;
            //INSERT INTO c_info_table (C_Name,C_Address,C_Url,C_Tel) VALUES("福鑫珠宝城", "商都路和顺广场一楼", "www.fuxin.com.cn", 0379 - 67688888);
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
            //to do
            //UPDATE c_info_table SET C_Status="Y" WHERE C_Id=2;
            return true;
        }

        //Delete a company
        public bool DeleteCompany(int id)
        {
            //to do
            //DELETE FROM c_info_table WHERE C_Id=1;
            return true;
        }
    }
}