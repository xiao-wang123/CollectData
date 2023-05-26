using System;
using System.Data;
using System.Data.OleDb;

namespace HBJYDataCollection.DBHelperClass
{
    public class AccessHelper
    {


        public string connString;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="path">access数据库文件路径</param>
        /// <param name="password">access数据库文件密码</param>
        public AccessHelper(string path, params object[] password)
        {
            if (password.Length < 1)
            {
                connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path;
            }
            else
            {
                connString = string.Format(@"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = {0}; Jet OLEDB:Database Password = {1};",
                                            path, password[0].ToString());

            }
        }


        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <returns>成功true；失败false</returns>
        public bool isConnected()
        {
            bool returnResult = false;
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    returnResult = true;
                }
                catch (OleDbException ex)
                {
                    throw ex;
                }
                finally
                {
                    conn.Close();
                }
            }
            return returnResult;
        }

        /// <summary>
        /// 执行查询语句，返回DataTable
        /// </summary>
        /// <param name="str">查询语句字符串</param>
        /// <returns></returns>
        public DataTable Query(string sqlString)
        {
            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    OleDbDataAdapter command = new OleDbDataAdapter(sqlString, conn);
                    command.Fill(dt);
                }
                catch (OleDbException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dt;
            }
        }




    }
}
