using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MvcFXProductMgr.Models
{
    public class Company
    {
        private int iNumberOfEntries = 1;
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Address { set; get; }
        public string Url { set; get; }
        public int Tel { set; get; }
        public int Status { set; get; }

        //Get:
        public Company GetCompany(int id)
        {
            return new Company();
        }

        public Company AddCompany(Company item){
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            item.Id = iNumberOfEntries + 1;
            //to do
            return item;
        }

        //Update
        public bool UpdateCompany(Company item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //to do

            return true;
        }

        //Delete a company
        public bool DeleteCompany(int id)
        {
            //to do
            return true;
        }
    }
}