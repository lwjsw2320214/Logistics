using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
namespace DBUtility
{
    public class HelpSQL
    {
        //数据库连接字符串(web.config来配置)
        private string connectionString;

        #region 获取连接字符串
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        public HelpSQL(string connString)
        {
            connectionString = ConfigurationManager.ConnectionStrings[connString].ConnectionString;
        }
        #endregion

        #region 获取SqlCommand对象
        /// <summary>
        /// 获取SqlCommand对象
        /// </summary>
        /// <param name="cmdText">要执行的SQL语句</param>
        /// <param name="commandType">执行方式</param>
        /// <param name="conn">数据库连接</param>
        /// <param name="parameter">参数</param>
        /// <param name="Trans">事物对象</param>
        /// <returns>DbCommand对象</returns>
        private void GetCommd(SqlCommand cmd, string cmdText, CommandType cmdType, SqlParameter[] parameter, SqlConnection conn, SqlTransaction Trans)
        {
            //打开数据库连接
            if (conn != null && conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            if (Trans != null)
            {

                cmd.Transaction = Trans;

            }

            cmd.Connection = conn;

            cmd.CommandText = cmdText;

            cmd.CommandType = cmdType;

            if (parameter != null)
            {
                cmd.Parameters.AddRange(parameter);
            }
        }
        #endregion

        #region 执行SQL语句得到一个Dataset
        /// <summary>
        /// 执行SQL语句得到一个Dataset
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public DataSet Query(string sql, CommandType cmdType, SqlParameter[] parameter)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    GetCommd(cmd, sql, cmdType, parameter, conn, null);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        try
                        {

                            da.Fill(ds, "ds");

                        }
                        catch (System.Data.SqlClient.SqlException e)
                        {

                            throw new Exception(e.Message);

                        }

                    }

                }

            }

            return ds;

        }
        #endregion

        #region 执行SQL语句得到一个Dataset
        /// <summary>
        /// 执行SQL语句得到一个Dataset
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public DataSet Query(string sql, SqlParameter[] parameter)
        {
            DataSet ds = new DataSet();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    GetCommd(cmd, sql, CommandType.Text, parameter, conn, null);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {

                        try
                        {

                            da.Fill(ds, "ds");

                        }
                        catch (System.Data.SqlClient.SqlException e)
                        {

                            throw new Exception(e.Message);

                        }

                    }

                }

            }

            return ds;

        }
        #endregion

        #region 执行SQL语句得到SqlDataReader
        /// <summary>
        /// 执行SQL语句得到SqlDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] parameter)
        {

            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand())
            {

                try
                {

                    GetCommd(cmd, sql, cmdType, parameter, conn, null);

                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);

                }
                catch (System.Data.SqlClient.SqlException)
                {

                    conn.Close();

                    throw;
                }

            }

        }
        #endregion

        #region 执行SQL语句得到SqlDataReader
        /// <summary>
        /// 执行SQL语句得到SqlDataReader
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(string sql, params SqlParameter[] parameter)
        {

            SqlConnection conn = new SqlConnection(connectionString);

            using (SqlCommand cmd = new SqlCommand())
            {

                try
                {

                    GetCommd(cmd, sql, CommandType.Text, parameter, conn, null);

                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);

                }
                catch (System.Data.SqlClient.SqlException)
                {

                    conn.Close();

                    throw;
                } 

            }

        }
        #endregion

        #region 执行一条SQL语句返回影响的行数
        /// <summary>
        /// 执行一条SQL语句返回影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, CommandType cmdType, params SqlParameter[] parameter)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    try
                    {

                        GetCommd(cmd, sql, cmdType, parameter, conn, null);

                        return cmd.ExecuteNonQuery();

                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        throw;
                    }

                }

            }

        }
        #endregion

        #region 执行一条SQL语句返回影响的行数
        /// <summary>
        /// 执行一条SQL语句返回影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public int ExecuteSql(string sql, params SqlParameter[] parameter)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    try
                    {

                        GetCommd(cmd, sql, CommandType.Text, parameter, conn, null);

                        return cmd.ExecuteNonQuery();

                    }
                    catch (System.Data.SqlClient.SqlException)
                    {
                        throw;
                    }

                }

            }

        }
        #endregion

        #region 使用事物批量执行sql返回影响的行数

        /// <summary>
        /// 使用事批量执行sql返回影响的行数
        /// </summary>
        /// <param name="sqlStringList">SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）</param>
        /// <returns></returns>
        public int ExecuteSqlTran(Hashtable sqlStringList)
        {
            var count = 0;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                //开启事物
                using (SqlTransaction trans = conn.BeginTransaction())
                {

                    using (SqlCommand cmd = new SqlCommand())
                    {

                        try
                        {
                            foreach (DictionaryEntry myde in sqlStringList)
                            {
                                GetCommd(cmd, myde.Key.ToString(), CommandType.Text, myde.Value as SqlParameter[], conn, trans);
                                count += cmd.ExecuteNonQuery();
                            }
                            trans.Commit();
                        }
                        catch
                        {
                            trans.Rollback();
                        }

                    }

                }

            }

            return count;
        }

        #endregion

        #region 批量执行sql，返回影响的行数
        public int ExecuteSqls(Hashtable sqlStringList)
        {
            var count = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    foreach (DictionaryEntry item in sqlStringList)
                    {
                        try
                        {
                            GetCommd(cmd, item.Key.ToString(), CommandType.Text, item.Value as SqlParameter[], conn, null);
                            count += cmd.ExecuteNonQuery();
                        }
                        catch { 
                        
                        }
                    }
                }
            }
            return count;
        }
        #endregion

        #region 执行一条SQL语句返回第一行第一列
        /// <summary>
        /// 执行一条SQL语句返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public int RunSql(string sql, CommandType cmdType, params SqlParameter[] parameter)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    GetCommd(cmd, sql, cmdType, parameter, conn, null);

                    return (int)cmd.ExecuteScalar();

                }

            }

        }
        #endregion

        #region 执行一条SQL语句返回第一行第一列
        /// <summary>
        /// 执行一条SQL语句返回第一行第一列
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="parameter">参数列表</param>
        /// <returns></returns>
        public int RunSql(string sql, params SqlParameter[] parameter)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand())
                {

                    GetCommd(cmd, sql, CommandType.Text, parameter, conn, null);

                    return (int)cmd.ExecuteScalar();

                }

            }

        }
        #endregion

        #region 单条数据启用手动开启数据库进行操作
        public SqlConnection Open(SqlConnection userconn)
        {
            userconn = new SqlConnection(connectionString);
            userconn.Open();
            return userconn;
        }

        public int ExecuteSqlOne(string sql, SqlParameter[] parameters, SqlConnection conn)
        {
            using (SqlCommand cmd = new SqlCommand())
            {
                try
                {
                    //GetCommd(cmd, sql, CommandType.Text, parameters, oneConn, null);
                    cmd.Connection = conn;
                    cmd.CommandText = sql;
                    cmd.CommandType = CommandType.Text;
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    return cmd.ExecuteNonQuery();
                }
                catch { throw; }
            }
        }

        public void Close(SqlConnection conn)
        {
            conn.Close();
        }
        #endregion
    }
}
