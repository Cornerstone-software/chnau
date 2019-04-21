using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace MvcFXProductMgr.Models
{
    public class AdvertmentModel
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public string Status { set; get; }

        /// <summary>
        /// 获取指定Id的广告信息详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AdvertmentModel GetAdvertmentById(int id)
        {
            string strCommandText = "SELECT A_Id AS AId,A_Name AS AName,A_Url AS AUrl FROM a_info_table WHERE A_Status='N'";
            strCommandText += " And A_Id='" + id.ToString() + "'";
            AdvertmentModel objAdvert = new AdvertmentModel();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    objAdvert.Id = id;
                    objAdvert.Name = dt.Rows[0]["AName"].ToString();
                    objAdvert.Url = dt.Rows[0]["AUrl"].ToString();
                }
                return objAdvert;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        /// <summary>
        /// 获取所有的广告信息
        /// </summary>
        /// <returns></returns>
        public List<AdvertmentModel> GetAllAdvertments()
        {
            string strCommandText = "SELECT A_Id AS AId,A_Name AS AName,A_Url AS AUrl FROM a_info_table WHERE A_Status='N'";
            List<AdvertmentModel> list = new List<AdvertmentModel>();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        AdvertmentModel advertObj = new AdvertmentModel();
                        advertObj.Id = Int32.Parse(dt.Rows[i]["AId"].ToString());
                        advertObj.Name = dt.Rows[i]["AName"].ToString();
                        advertObj.Url = dt.Rows[i]["AUrl"].ToString();
                        list.Add(advertObj);
                    }
                }
                else
                {
                    AdvertmentModel objAdvert = new AdvertmentModel();
                    objAdvert.Id = Int32.Parse("1");
                    objAdvert.Name = "";
                    objAdvert.Url = "";
                    list.Add(objAdvert);
                }
                return list;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message.ToString());
                AdvertmentModel objAdvert = new AdvertmentModel();
                objAdvert.Id = Int32.Parse("1");
                objAdvert.Name = "";
                objAdvert.Url = "";
                list.Add(objAdvert);

                return list;
            }
        }

        public bool AddAdvertment(AdvertmentModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            string strCommandText = "INSERT INTO a_info_table (A_Name,A_Url) VALUES(";
            strCommandText += "\'" + item.Name + "\',";
            strCommandText += "\'" + item.Url + "\'";
            strCommandText += ");";
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

        //Update
        public bool UpdateAdvertment(AdvertmentModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //to do

            //UPDATE a_info_table SET A_Status="Y" WHERE A_Id=2;
            string strCommandText = "UPDATE a_info_table SET";
            strCommandText += " A_Name ='" + item.Name + "',";
            strCommandText += " A_Url='" + item.Url + "'";
            strCommandText += " WHERE A_Id=" + item.Id;
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
        public void DeleteAdvertment(int id)
        {
            string strCommandText = "UPDATE a_info_table SET A_Status='X' WHERE";
            strCommandText += " A_Id=" + id;
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