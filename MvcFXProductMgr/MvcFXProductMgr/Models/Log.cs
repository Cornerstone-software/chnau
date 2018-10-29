using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcFXProductMgr.Models
{
    public class Log
    {
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }//操作员
        public DateTime Date { set; get; }
        public string Content { set; get; }

        public Log GetLog(int id)
        {
            return new Log();
        }

        public Log AddLog(Log item){

            return item;
        }
    }
}