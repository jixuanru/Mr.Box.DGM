using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using Aspose.Cells;

namespace Mr.Box.DGM.Common
{
    /// <summary>
    /// DataTable帮助类
    /// </summary>
    /// <typeparam name="T">泛类</typeparam>
    public  class DataTableHelper
    {
        //public  class DataTableHelper<T> where T : new()
        #region Public Fields

        private static string _exceptionMessage; //定义异常信息

        #endregion

        #region Preperty


       
        /// <summary>
        /// 设置或取得异常信息
        /// </summary>
        public string ExceptionMessage
        {
            get { return _exceptionMessage; }
            set { _exceptionMessage = value; }
        }

        #endregion

        #region Method

        /// <summary>
        /// 连接两个具有相同数据结构的DataTable,返回DataTable
        /// </summary>
        /// <param name="table1">表1</param>
        /// <param name="table2">表2</param>
        /// <returns>返回表</returns>
        public static DataTable GetInnerDataTable(DataTable table1, DataTable table2)
        {
            table1.Merge(table2);
            return table1;
        }

        /// <summary>
        /// 将数据集写入xml文件
        /// </summary>
        /// <param name="dataset">数据集</param>
        /// <param name="filename">XML文件名</param>
        /// <returns>是否成功</returns>
        public static bool WriteDataSetToXml(DataSet dataset, string filename)
        {
            try
            {
                dataset.WriteXml(filename);
                return true;
            }
            catch (Exception ex)
            {
                _exceptionMessage = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 查询DataTable中的数据
        /// </summary>
        /// <param name="table">要查询的datatable</param>
        /// <param name="comText">查询条件</param>
        /// <returns>数据列集合</returns>
        public static DataRow[] GetSelectDataTable(DataTable table, string comText)
        {
            try
            {
                DataRow[] rows = table.Select(comText);
                return rows;
            }
            catch (Exception ex)
            {
                _exceptionMessage = ex.Message;
                return null;
            }
        }

        

       

        /// <summary>
        /// 枚举转DataTable
        /// </summary>
        /// <param name="enumType">类型</param>
        /// <param name="key">索引</param>
        /// <param name="val">值</param>
        /// <returns>DataTable</returns>
        public static DataTable EnumToDataTable(Type enumType, string key, string val)
        {
            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType);

            var table = new DataTable();
            table.Columns.Add(key, Type.GetType("System.String"));
            table.Columns.Add(val, Type.GetType("System.Int32"));
            table.Columns[key].Unique = true;
            for (int i = 0; i < values.Length; i++)
            {
                var dr = table.NewRow();
                dr[key] = names[i];
                dr[val] = (int)values.GetValue(i);
                table.Rows.Add(dr);
            }
            return table;
        }

        /// <summary>
        /// 根据nameList里面的字段创建一个表格,返回该表格的DataTable
        /// </summary>
        /// <param name="nameList">包含字段信息的列表</param>
        /// <returns>DataTable</returns>
        public static DataTable CreateTable(List<string> nameList)
        {
            if (nameList.Count <= 0)
                return null;

            var myDataTable = new DataTable();
            foreach (string columnName in nameList)
            {
                myDataTable.Columns.Add(columnName, typeof(string));
            }
            return myDataTable;
        }

        /// <summary>
        /// 通过字符列表创建表字段，字段格式可以是：
        /// 1) a,b,c,d,e
        /// 2) a|int,b|string,c|bool,d|decimal
        /// </summary>
        /// <param name="nameString">表名</param>
        /// <returns>数据表</returns>
        public static DataTable CreateTable(string nameString)
        {
            string[] nameArray = nameString.Split(new[] { ',', ';' });
            new List<string>();
            var dt = new DataTable();
            foreach (string item in nameArray)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] subItems = item.Split('|');
                    if (subItems.Length == 2)
                    {
                        dt.Columns.Add(subItems[0], ConvertType(subItems[1]));
                    }
                    else
                    {
                        dt.Columns.Add(subItems[0]);
                    }
                }
            }
            return dt;
        }

        /// <summary>
        /// 排序表的视图
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="sorts">排序字段</param>
        /// <returns>排序后的表</returns>
        public static DataTable SortedTable(DataTable dt, params string[] sorts)
        {
            if (dt.Rows.Count > 0)
            {
                string tmp = sorts.Aggregate("", (current, t) => current + (t + ","));
                dt.DefaultView.Sort = tmp.TrimEnd(',');
            }
            return dt;
        }
        /// <summary>
        /// 导出Excel DataTable转换Excel
        /// </summary>
        /// <param name="tmpDataTable">DataTable </param>
        /// <param name="strFilePath">文件路径(不包含文件名)</param>
        /// <param name="strFileName">文件名</param>
        public static void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFilePath, string strFileName)
        {
            if (tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 0;
            int columnIndex = 0;
            Workbook xlApp = new Workbook();
            foreach (DataColumn dc in tmpDataTable.Columns)
            {
                xlApp.Worksheets[0].Cells[rowIndex, columnIndex].PutValue(dc.ColumnName);
                columnIndex++;
            }
            for (int i = 0; i < rowNum; i++)
            {
                for (int j = 0; j < columnNum; j++)
                {
                    xlApp.Worksheets[0].Cells[i + 1, j].PutValue(tmpDataTable.Rows[i][j].ToString());
                }
            }
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }
            xlApp.Save(strFilePath + strFileName);
        }



        public static DataTable ListToDataTable(IList list)
        {
            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    result.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }
            return result;
        }


        private static Type ConvertType(string typeName)
        {
            typeName = typeName.ToLower().Replace("system.", "");
            Type newType = typeof(string);
            switch (typeName)
            {
                case "boolean":
                case "bool":
                    newType = typeof(bool);
                    break;
                case "int16":
                case "short":
                    newType = typeof(short);
                    break;
                case "int32":
                case "int":
                    newType = typeof(int);
                    break;
                case "long":
                case "int64":
                    newType = typeof(long);
                    break;
                case "uint16":
                case "ushort":
                    newType = typeof(ushort);
                    break;
                case "uint32":
                case "uint":
                    newType = typeof(uint);
                    break;
                case "uint64":
                case "ulong":
                    newType = typeof(ulong);
                    break;
                case "single":
                case "float":
                    newType = typeof(float);
                    break;

                case "string":
                    newType = typeof(string);
                    break;
                case "guid":
                    newType = typeof(Guid);
                    break;
                case "decimal":
                    newType = typeof(decimal);
                    break;
                case "double":
                    newType = typeof(double);
                    break;
                case "datetime":
                    newType = typeof(DateTime);
                    break;
                case "byte":
                    newType = typeof(byte);
                    break;
                case "char":
                    newType = typeof(char);
                    break;
            }
            return newType;
        }
        public static string DtToJson(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            ArrayList dic = new ArrayList();
            foreach (DataRow row in dtb.Rows)
            {
                Dictionary<string, object> drow = new Dictionary<string, object>();
                foreach (DataColumn col in dtb.Columns)
                {
                    drow.Add(col.ColumnName, row[col.ColumnName]);
                }
                dic.Add(drow);
            }
            jss.MaxJsonLength = Int32.MaxValue;
            return jss.Serialize(dic);
        }

        #endregion
    }
}
