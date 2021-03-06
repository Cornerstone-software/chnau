﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;


namespace MvcFXProductMgr.Models
{
    public class TestingOrgModel
    {
        public int Id { set; get; }
         [Required]
        [Display(Name = "检测机构名称")]
        public string Name { set; get; }
        public string Url { set; get; }
        public string Tel { set; get; }
        public string Status { set; get; }

        //Get:
        public TestingOrgModel GetTestingOrgById(int id)
        {
            string strCommandText = "SELECT T_Id AS TId,T_Name AS TName,T_Url AS TUrl,T_Tel AS TTel FROM t_info_table WHERE T_Status='N'";
            strCommandText += " And T_Id='" + id.ToString() + "'";
            TestingOrgModel testingOrgObj = new TestingOrgModel();
            try {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    testingOrgObj.Id = id;
                    testingOrgObj.Name = dt.Rows[0]["TName"].ToString();
                    testingOrgObj.Url = dt.Rows[0]["TUrl"].ToString();
                    testingOrgObj.Tel = dt.Rows[0]["TTel"].ToString();
                }
                return testingOrgObj;
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }

           

        }
        /// <summary>
        /// 获取所有检测机构的信息
        /// </summary>
        /// <returns>List<TestingOrgModel></returns>
        public List<TestingOrgModel> GetAllTestingOrgs()
        {
            string strCommandText = "SELECT T_Id AS TId,T_Name AS TName,T_Url AS TUrl,T_Tel AS TTel FROM t_info_table WHERE T_Status='N'";
            List<TestingOrgModel> list = new List<TestingOrgModel>();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TestingOrgModel testingOrgObj = new TestingOrgModel();
                        testingOrgObj.Id = Int32.Parse(dt.Rows[i]["TId"].ToString());
                        testingOrgObj.Name = dt.Rows[i]["TName"].ToString();
                        testingOrgObj.Url = dt.Rows[i]["TUrl"].ToString();
                        testingOrgObj.Tel = dt.Rows[i]["TTel"].ToString();
                        list.Add(testingOrgObj);
                    }
                }
                else
                {
                    TestingOrgModel objTesting = new TestingOrgModel();
                    objTesting.Id = Int32.Parse("1");
                    objTesting.Name = "河南省金银珠宝饰品质量监督检验中心";
                    list.Add(objTesting);
                }
                return list;
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message.ToString());
                TestingOrgModel objTesting = new TestingOrgModel();
                objTesting.Id = Int32.Parse("1");
                objTesting.Name = "河南省金银珠宝饰品质量监督检验中心";
                list.Add(objTesting);
                
                return list;
            }
        }
        //Add:
        public bool AddTestingOrg(TestingOrgModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            string strCommandText = "INSERT INTO t_info_table (T_Name,T_Url,T_Tel) VALUES(";
            strCommandText += "\'" + item.Name + "\',";
            strCommandText += "\'" + item.Url + "\',";
            strCommandText += "\'" + item.Tel + "\'";
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
        public bool UpdateTestingOrg(TestingOrgModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //UPDATE t_info_table SET T_Status="Y" WHERE T_Id=2;
            string strCommandText = "UPDATE t_info_table SET";
            strCommandText += " T_Name ='" + item.Name + "',";
            strCommandText += " T_Url='" + item.Url + "',";
            strCommandText += " T_Tel='" + item.Tel + "'";
            strCommandText += " WHERE T_Id=" + item.Id;
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

        //Delete
        public void DeleteTestingOrg(int id)
        {
            string strCommandText = "UPDATE t_info_table SET T_Status='X' WHERE";
            strCommandText += " T_Id=" + id;
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