using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using MvcFXProductMgr.Models;
namespace MvcFXProductMgr.ViewModels
{
    public class ProductViewModel:ProductModel
    {
        public Int32 Exist { set; get; }
    }
}