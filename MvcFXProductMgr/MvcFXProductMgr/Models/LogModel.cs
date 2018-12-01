using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data;
using MvcFXProductMgr.ViewModels;
namespace MvcFXProductMgr.Models
{
    public class LogModel
    {
        
        [Key]
        public int Id { set; get; }
        public string Name { set; get; }//操作员
        public DateTime Date { set; get; }
        public string Content { set; get; }

        public LogModel()
        {

        }
       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <returns></returns>
        public LogModel GetLog(int id)
        {
            return new LogModel();
        }

        public LogViewModel GetAllLogs(string userName)
        {
            LogViewModel logViewModel = new LogViewModel();
            string strCommandText = "select * from l_info_table where L_Name=";
            strCommandText += "\'" + userName + "\'";
            try
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);

                if (dt.Rows.Count>0)
                {                                   
                  List<LogModel> list = new List<LogModel>();
                    logViewModel.LogList = ConvertHelper<LogModel>.DataTableToList(dt);
                    return logViewModel;
                }
                else
                {
                    logViewModel.LogList = new List<LogModel>();     
                }
                return logViewModel;
                     
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            

        }
        public bool AddLog(LogModel item){
            
            string strCommandText = "insert into l_info_table(L_Name,L_Content,L_Date) values(";
            strCommandText += "\"" + item.Name + "\",";
            strCommandText += "\"" + item.Content + "\",";
            strCommandText += "\"" + item.Date.ToString() + "\"";
            strCommandText += ")";
            try
            {
                int iResult = MySQLHelper.ExecuteNonQuery(MySQLHelper.Conn, System.Data.CommandType.Text, strCommandText, null);
                if (iResult > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
                
            }
            
            
        }
    }
}