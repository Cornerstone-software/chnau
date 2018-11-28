using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFXProductMgr.Models
{
    public class AdvertmentModel
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }
        public string Url { set; get; }
        public string Status { set; get; }

        //Get:
        public AdvertmentModel GetAdvertment(int id)
        {
            return new AdvertmentModel();
        }

        public AdvertmentModel AddAdvertment(AdvertmentModel item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

           
            //to do
            return item;
        }

        //Update
        public bool UpdateAdvertment(AdvertmentModel item)
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