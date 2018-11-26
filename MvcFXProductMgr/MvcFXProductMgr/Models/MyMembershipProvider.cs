using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Web.Security;
using MySql.Data.MySqlClient;
namespace MvcFXProductMgr.Models
{
    #region MyMembershipProvider //自定义
    public  class MyMembershipProvider:MembershipProvider
    {
        private string conn;
        private int minRequiredPasswordLength;
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public MyMembershipProvider()
        {
            conn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            MySqlHelper.Conn = conn;
            this.minRequiredPasswordLength = 6;
            
        }
        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                bool isExist = this.ValidateUser(username, oldPassword);
                if (isExist)
                {
                    string strCommandText = "update u_info_table set U_Password=";
                    strCommandText += "\'"+newPassword+"\'";
                    strCommandText += "where U_Name=" + "\'" + username + '\'';
                    int iResult = MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, strCommandText, null);;
                    if (iResult > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                    
                }
                else
                {
                    return false;
                }
            }
            catch(Exception ex){
                throw new Exception(ex.Message);
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="passwordQuestion"></param>
        /// <param name="passwordAnswer"></param>
        /// <param name="isApproved"></param>
        /// <param name="providerUserKey"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public override MembershipUser CreateUser(string username, string password, string email,string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {

            string strCommandText = "insert into u_info_table(U_Name,U_Password) values(";
            strCommandText += "\"" + username + "\",";
            strCommandText += "\"" + password + "\"";
            strCommandText += ")";
            
            try
            {
                int iresult= MySqlHelper.ExecuteNonQuery(MySqlHelper.Conn, CommandType.Text, strCommandText, null);
                if (iresult > 0)
                {
                    MembershipUser user = new MembershipUser("MyMembershipProvider", username, providerUserKey, null, passwordQuestion, "", isApproved, true, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now);
                    status = MembershipCreateStatus.Success;
                    return user;

                }
                else
                {
                    status = MembershipCreateStatus.UserRejected;
                    return null;
                }
            }
            catch (Exception ex) 
            {
                status = MembershipCreateStatus.ProviderError;
                throw new Exception(ex.Message); 
            }
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotImplementedException();
        }

        public override bool EnablePasswordReset
        {
            get { throw new NotImplementedException(); }
        }

        public override bool EnablePasswordRetrieval
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotImplementedException();
        }
        public bool GetUser(string username)
        {
            try
            {

                string str_mysql = "SELECT * FROM u_info_table where U_Name=\'" + username + '\'';
                DataSet ds = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, str_mysql, null);
                if (ds.Tables[0].Rows.Count > 0)
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
        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotImplementedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { return this.minRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotImplementedException(); }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotImplementedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { throw new NotImplementedException(); }
        }

        public override bool RequiresUniqueEmail
        {
            get { throw new NotImplementedException(); }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotImplementedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotImplementedException();
        }

        public override void UpdateUser(MembershipUser user)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateUser(string username, string password)
        {
            
            try
            {

                string str_mysql = "SELECT * FROM u_info_table where U_Name=\'" + username + "\' and U_Password=\'" + password

                + '\'';
                DataSet ds = MySqlHelper.GetDataSet(MySqlHelper.Conn, CommandType.Text, str_mysql, null);
                if (ds.Tables[0].Rows.Count > 0)
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
    #endregion
    
}