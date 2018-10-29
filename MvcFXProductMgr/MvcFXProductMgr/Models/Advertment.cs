using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFXProductMgr.Models
{
    public class Advertment
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public string Status { set; get; }

        //Get:
        public Advertment GetAdvertment(int id)
        {
            return new Advertment();
        }

        public Advertment AddAdvertment(Advertment item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

           
            //to do
            return item;
        }

        //Update
        public bool UpdateAdvertment(Advertment item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            //to do

            return true;
        }

        //Delete a company
        public bool DeleteAdvertment(int id)
        {
            //to do
            return true;
        }
    }
}