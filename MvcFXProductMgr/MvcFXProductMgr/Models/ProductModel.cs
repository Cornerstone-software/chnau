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
        public string Barcode { set; get; }
        [Key]
        public string CerNum { set; get; }
        public string Name { set; get; }
        public float Weight { set; get; }
        public int Price { set; get; }
        public string Standard { set; get; }
        public string Category { set; get; }

        public int CId { set; get; }
        public string CName { set; get; }
        public string CAddress { set; get; }
        public string CUrl { set; get; }
        public string CTel { set; get; }

        public int TId { set; get; }
        public string TName { set; get; }
        public string TUrl { set; get; }
        public string TTel { set; get; }

        public int AId { set; get; }
        public string AName { set; get; }
        public string AUrl { set; get; }
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
        public ProductModel GetProductDetail(int id)
        {
            throw new NotImplementedException();
        }
    }
}