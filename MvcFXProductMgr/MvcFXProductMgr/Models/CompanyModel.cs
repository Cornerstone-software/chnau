using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
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

        //Get:
        public CompanyModel GetCompany(int id)
        {
            return new CompanyModel();
        }

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

        //Update
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