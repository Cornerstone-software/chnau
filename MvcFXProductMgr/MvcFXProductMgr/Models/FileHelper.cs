using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace MvcFXProductMgr.Models
{
    public class FileHelper
    {
        public FileHelper()
        {
        }
         private const int OF_READWRITE = 2;
        private const int OF_SHARE_DENY_NONE = 0x40;
        private static readonly IntPtr HFILE_ERROR = new IntPtr(-1);
 
         
         /// <summary>
         /// 判断文件是否打开
         /// </summary>
         /// <param name="lpPathName">文件名称</param>
         /// <param name="iReadWrite"></param>
         /// <returns></returns>
         [DllImport("kernel32.dll")]
         private static extern IntPtr _lopen(string lpPathName, int iReadWrite);
 
         /// <summary>
         /// 关闭文件句柄
         /// </summary>
         /// <param name="hObject"></param>
         /// <returns></returns>
         [DllImport("kernel32.dll")]
        private static extern bool CloseHandle(IntPtr hObject);
        /// <summary>
        /// 获取进程
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="ID"></param>
        /// <returns></returns>
         [DllImport("User32.dll", CharSet = CharSet.Auto)]
         public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);

        /// <summary>
        /// 文件名称
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
         public static bool IsFileOpen(string filename)
         {
             IntPtr vHandle = _lopen(filename, OF_READWRITE | OF_SHARE_DENY_NONE);
             if (vHandle == HFILE_ERROR)
             {
                 CloseHandle(vHandle);
                 return true;
            }
             else
                return false;
            
         }


    }
}