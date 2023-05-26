using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;


namespace HBJYDataCollection.DBHelperClass
{
    public class OracleHelper
    {
        public string connString = string.Empty;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host">IP地址</param>
        /// <param name="port">端口号</param>
        /// <param name="dataBase">数据源</param>
        /// <param name="user">用户名</param>
        /// <param name="passWord">密码</param>
        public OracleHelper(string host, string port, string dataBase, string user, string passWord)
        {
            connString = string.Format(@"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = {2})));User ID={3};Password={4};", host, port, dataBase, user, passWord);
            //connString = string.Format(@"Data Source=(DESCRIPTION = (ADDRESS = (PROTOCOL = TCP)(HOST = {0})(PORT = {1}))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = {2})));User ID={3};Password={4};", host, port, dataBase, user, passWord);
        }

        /// <summary>
        /// 数据库连接测试
        /// </summary>
        /// <returns>成功true；失败false</returns>
        public bool isConnected()
        {
            bool returnResult = false;
            using (OracleConnection conn = new OracleConnection(connString))
            {
                try
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                        returnResult = true;
                    }
                }
                catch (OracleException ex)
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
            using (OracleConnection conn = new OracleConnection(connString))
            {
                DataTable dt = new DataTable();
                try
                {
                    conn.Open();
                    OracleDataAdapter command = new OracleDataAdapter(sqlString, conn);
                    command.Fill(dt);
                }
                catch (OracleException ex)
                {
                    throw new Exception(ex.Message);
                }
                return dt;
            }
        }

        /// <summary>
        /// 执行单条SQL语句
        /// </summary>
        /// <param name="str">单条SQL语句</param>
        /// <returns>成功TRUE，失败FALSE</returns>
        public bool ExecuteSql(string sqlString)
        {
            using (OracleConnection Conn = new OracleConnection(connString))
            {
                using (OracleCommand cmd = new OracleCommand(sqlString, Conn))
                {
                    try
                    {
                        if (Conn.State == ConnectionState.Closed)
                        {
                            Conn.Open();
                        }
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                    catch (OracleException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        Conn.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务。
        /// </summary>
        /// <param name="sqlStringList">多条SQL语句</param>	
        public int ExecuteSqlTran(List<String> sqlStringList)
        {
            using (OracleConnection conn = new OracleConnection(connString))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                OracleTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < sqlStringList.Count; n++)
                    {
                        string strsql = sqlStringList[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch
                {
                    tx.Rollback();
                    return -1;
                }
                finally
                {
                    conn.Close();
                }
            }
        }

    }
}
