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
        /// 获取操作日志列表
        /// </summary>
        /// <param name="userName">操作员登录账号</param>
        /// <param name="content">操作内容，如：Logo,AddProducts</param>
        /// <returns></returns>
        public LogViewModel GetLogsByNameAndContent(string userName, string content)
        {
            LogViewModel logViewModel = new LogViewModel();
            string strCommandText = "SELECT L_Name AS userName,L_Date AS DATE,L_Content AS content FROM l_info_table WHERE";
            strCommandText += " L_Name='" + userName + "'";
            strCommandText +=" AND L_Content='" + content + "'";
            try 
            {
                DataTable dt = MySQLHelper.GetDataTable(MySQLHelper.Conn, CommandType.Text, strCommandText, null);

                if (dt.Rows.Count > 0)
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

        public LogViewModel GetLogsByName(string userName)
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
        public void AddLog(LogModel item){
            
            string strCommandText = "insert into l_info_table(L_Name,L_Content,L_Date) values(";
            strCommandText += "\"" + item.Name + "\",";
            strCommandText += "\"" + item.Content + "\",";
            strCommandText += "\"" + item.Date.ToString() + "\"";
            strCommandText += ")";
            try
            {
                int iResult = MySQLHelper.ExecuteNonQuery(MySQLHelper.Conn, System.Data.CommandType.Text, strCommandText, null);
                if (iResult < 1)
                {
                    throw new Exception("操作日志记录创建失败");
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
                
            }
            
            
        }
    }
}