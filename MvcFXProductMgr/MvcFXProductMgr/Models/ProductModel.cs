using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFXProductMgr.Models
{
    public class ProductModel
    {
        private int iNumberOfEntries = 1;
        [Key]
        public int Id { set; get; }
        public int Barcode { set; get; }
        [Key]
        public int Cer_Num { set; get; }
        public string Name { set; get; }
        public float Weight { set; get; }
        public int Price { set; get; }
        public string Standard { set; get; }
        public string Category { set; get; }
        public int C_Id { set; get; }

        //public string C_Name { set; get; }
        //public string C_Address { set; get; }
       // public string C_Url { set; get; }
       // public int C_Tel { set; get; }

        public int T_Id { set; get; }
       // public string T_Name { set; get; }
       // public string T_Url { set; get; }
       // public int T_Tel { set; get; }

        public int A_Id { set; get; }
       // public string A_Name { set; get; }
       // public string A_Url { set; get; }

        public string Remarks { set; get; }

        public string Status { set; get; }

        //Get:
        public ProductModel GetProduct(int cer_num,string name)
        {
            return new ProductModel();
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

    }
}