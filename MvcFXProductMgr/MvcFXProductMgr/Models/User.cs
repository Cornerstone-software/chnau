using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace MvcFXProductMgr.Models
{
    public class User
    {
        [Key]
        public int Id { set; get; }
         [Key]
        public string Name { set; get; }
        public string Password { set; get; }
        public string Status { set; get; }

        public User GetUser(string Name)
        {
            User userObj = new User();
            //to do
            return userObj;
            
        }
        public User RegisterUser(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            //to do
            
            return item;

        }

        //Update Password
        public bool ChangePassword(User item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            //to do

            return true;

        }
        //Delete User
        public bool DeleteUser(string name)
        {
            //验证用户是否存在

            //提示是否确定要删除
            return true;
        }


    }
}