using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Collections;

namespace MvcFXProductMgr.Models
{
    /// <summary>
    /// DataTable 和List<T>相互转化类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConvertHelper<T> where T : new()
    {
        #region DataTableToList
        /// <summary>
        /// DataTable转化为List<Model>
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList(DataTable dt)
        {
            
            if (dt == null||dt.Rows.Count<0)
            {
                return null;
            }
            // 返回值初始化 
            List<T> result = new List<T>();
 
            // 获得此模型的类型
            Type type = typeof(T);
            //定义一个临时变量
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量
                    //检查DataTable是否包含此列（列名==对象的属性名）  
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出
                        //取值
                        object value = dr[tempName];
                        //转化datatable 列值的类型，使其与对象的属性的类型一致
                        if (pi.PropertyType.FullName == typeof(DateTime).FullName) value = DateTime.Parse(value.ToString());
                        if (pi.PropertyType.FullName == typeof(Int32).FullName)
                        {
                            if (value == DBNull.Value) 
                            { 
                                value = Int32.Parse("0");
                            }
                            else
                            {
                                value = Int32.Parse(value.ToString());
                            }                     
                        }
                        if (pi.PropertyType.FullName == typeof(Single).FullName)
                        {
                            if (value == DBNull.Value)
                            {
                                value = Single.Parse("0.00");
                            }
                            else
                            {
                                value = Single.Parse(value.ToString());
                            }
                        }
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中
                result.Add(t);
            }

            return result;
        }
        #endregion
        #region List to DataTable
        public static DataTable ListToDataTable(List<T> list)
        {
            var props = typeof(T).GetProperties();
            var dt = new DataTable();
            dt.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());
            if (list.Count() > 0)
            {
                for (int i = 0; i < list.Count(); i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in props)
                    {
                        object obj = pi.GetValue(list.ElementAt(i), null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    dt.LoadDataRow(array, true);
                }
            }
            return dt;

        }
        #endregion
    }
}