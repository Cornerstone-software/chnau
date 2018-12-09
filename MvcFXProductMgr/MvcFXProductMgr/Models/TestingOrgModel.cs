using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace MvcFXProductMgr.Models
{
    public class TestingOrgModel
    {
       // private int iNumberOfEntries = 1;
        public int Id { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public int Tel { set; get; }
        public string Status { set; get; }

        //Get:
        public TestingOrgModel GetTestingOrg(int id)
        {
            TestingOrgModel testingOrgObj = new TestingOrgModel();
            return testingOrgObj;

        }
        /// <summary>
        /// 获取所有检测机构的信息
        /// </summary>
        /// <returns>List<TestingOrgModel></returns>
        public List<TestingOrgModel> GetAllTestingOrgs()
        {
            string strCommandText = "SELECT T_Id AS TId,T_Name AS TName FROM t_info_table WHERE T_Status='N'";
            List<TestingOrgModel> list = new List<TestingOrgModel>();
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TestingOrgModel objCompany = new TestingOrgModel();
                        objCompany.Id = Int32.Parse(dt.Rows[i]["TId"].ToString());
                        objCompany.Name = dt.Rows[i]["TName"].ToString();
                        list.Add(objCompany);
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
        public TestingOrgModel AddTestingOrg(TestingOrgModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            return item;
        }

        //Update
        public bool UpdateTestingOrg(TestingOrgModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            return true;
        }

        //Delete
        public bool DeleteTestingOrg(int id)
        {
            //to do

            return true;

        }

    }
}