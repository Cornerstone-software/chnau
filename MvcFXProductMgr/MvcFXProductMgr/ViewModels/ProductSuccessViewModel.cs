using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFXProductMgr.Models;
namespace MvcFXProductMgr.ViewModels
{
    public class ProductSuccessViewModel
    {
        public List<ProductViewModel> ProductList { get; set; }
        public string UploadDateTime { get; set; }
        public string UploadUserName { get; set; }
        public Int32 UploadNum { get; set; }
        public string Category { get; set; }
        public string Standard { get; set; }
        public List<SelectListItem> Company { get; set; }
        public List<SelectListItem> TestingOrg { get; set; }
    }
    

}