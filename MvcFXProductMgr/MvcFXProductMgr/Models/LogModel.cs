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
            string strCommandText = "select * from l_info_table where L_Name=";
            strCommandText += "\'" + userName + "\'";
            try
            {
                DataSet ds = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, strCommandText, null);
                if (ds.Tables[0].Rows.Count>0)
                { 
                    DataTable dt =ds.Tables[0];
                    LogViewModel logViewModel = new LogViewModel();
                    List<LogModel> list = new List<LogModel>();
                    for(int i=0;i<dt.Rows.Count;i++){
                        LogModel objLog = new LogModel();
                        objLog.Id = Int32.Parse(dt.Rows[i]["L_Id"].ToString());
                        objLog.Name = dt.Rows[i]["L_Name"].ToString();
                        objLog.Content = dt.Rows[i]["L_Content"].ToString();
                        objLog.Date = DateTime.Parse(dt.Rows[i]["L_Date"].ToString());
                        list.Add(objLog);

                    }
                    logViewModel.LogList = list;
                    return logViewModel;
                }
                else
                {
                    return new LogViewModel();
                }
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
                int iResult = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, System.Data.CommandType.Text, strCommandText, null);
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