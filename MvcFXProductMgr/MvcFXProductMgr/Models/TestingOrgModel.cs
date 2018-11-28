using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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