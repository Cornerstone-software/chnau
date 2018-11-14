using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcFXProductMgr.Models
{
    public class TestingOrg
    {
       // private int iNumberOfEntries = 1;
        public int Id { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public int Tel { set; get; }
        public string Status { set; get; }

        //Get:
        public TestingOrg GetTestingOrg(int id)
        {
            TestingOrg testingOrgObj = new TestingOrg();
            return testingOrgObj;

        }

        //Add:
        public TestingOrg AddTestingOrg(TestingOrg item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            return item;
        }

        //Update
        public bool UpdateTestingOrg(TestingOrg item)
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