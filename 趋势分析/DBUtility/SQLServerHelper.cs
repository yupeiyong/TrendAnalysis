using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DBUtility
{
    public class SQLServerHelper:IDisposable 
    {
        /// <summary>
        /// 数据库连接对象
        /// </summary>
        private SqlConnection conn = null;
        /// <summary>
        /// 数据返回参数名称
        /// </summary>
        private readonly string RETURN_VALUE = "RETURN_VALUE";
        /// <summary>
        /// 打开数据库连接
        /// </summary>
        public void Open()
        {
            if (conn == null)
            {
                conn = new SqlConnection(ConnectionString.GetConnString());
            }
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        /// <summary>
        /// 关闭数据库连接
        /// </summary>
        public void Close()
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (conn != null)
            {
                conn.Dispose();
                conn = null;
            }
        }

        public int RunProc(string procName)
        {
            SqlCommand cmd = CreateProcCmd(procName, null);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
            //返回值
            return (int)cmd.Parameters[RETURN_VALUE].Value;
        }
        public int RunProc(string procName, SqlParameter[] pars)
        {
            SqlCommand cmd = CreateProcCmd(procName, pars);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
            //返回值
            return (int)cmd.Parameters[RETURN_VALUE].Value;
        }
        public void RunProc(string procName,out SqlDataReader reader)
        {
            SqlCommand cmd = CreateProcCmd(procName, null);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RunProc(string procName, SqlParameter[] pars, out SqlDataReader reader)
        {
            SqlCommand cmd = CreateProcCmd(procName, pars);
            try
            {
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RunProc(string procName, ref DataSet dataset)
        {
            if (dataset == null)
            {
                dataset = new DataSet();
            }
            SqlDataAdapter adapter = CreateProcAdapter(procName, null);
            try
            {
                adapter.Fill(dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public void RunProc(string procName, SqlParameter[] pars, ref DataSet dataset)
        {
            if (dataset == null)
            {
                dataset = new DataSet();
            }
            SqlDataAdapter adapter = CreateProcAdapter(procName, pars);
            try
            {
                adapter.Fill(dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public int RunSQL(string sqlstr)
        {
            SqlCommand cmd = CreateSQLCmd(sqlstr, null);
            int result = 0;
            try
            {
                result=cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
            //返回值
            return result;
        }
        public int RunSQL(string sqlstr, SqlParameter[] pars)
        {
            SqlCommand cmd = CreateSQLCmd(sqlstr, pars);
            int result = 0;
            try
            {
                result=cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
            //返回值
            return result;
        }
        public void RunSQL(string sqlstr, out SqlDataReader reader)
        {
            SqlCommand cmd = CreateSQLCmd(sqlstr, null);
            try
            {
                reader=cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RunSQL(string sqlstr, SqlParameter[] pars, out SqlDataReader reader)
        {
            SqlCommand cmd = CreateSQLCmd(sqlstr, pars);
            try
            {
                reader = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RunSQL(string sqlstr, ref DataSet dataset)
        {
            if (dataset == null)
            {
                dataset = new DataSet();
            }
            SqlDataAdapter adapter = CreateSQLAdapter(sqlstr, null);
            try
            {
                adapter.Fill(dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        public void RunSQL(string sqlstr, SqlParameter[] pars, ref DataSet dataset)
        {
            if (dataset == null)
            {
                dataset = new DataSet();
            }
            SqlDataAdapter adapter = CreateSQLAdapter(sqlstr, pars);
            try
            {
                adapter.Fill(dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close();
            }
        }
        private SqlCommand CreateProcCmd(string procName, SqlParameter[] pars)
        {
            //打开数据库连接
            Open();
            //命令对象
            SqlCommand cmd = new SqlCommand(procName, conn);
            //存储过程
            cmd.CommandType = CommandType.StoredProcedure;
            if (pars != null)
            {
                for (int i = 0; i < pars.Length; i++)
                {
                    cmd.Parameters.Add(pars[i]);
                }
            }
            cmd.Parameters.Add(new SqlParameter(
                    RETURN_VALUE,
                    SqlDbType.Int,
                    4,
                    ParameterDirection.ReturnValue,
                    false,
                    0,
                    0,
                    string.Empty,
                    DataRowVersion.Default,
                    null
                )
            );
            return cmd;
        }
        private SqlCommand CreateSQLCmd(string sqlstr, SqlParameter[] pars)
        {
            //打开数据库连接
            Open();
            //命令对象
            SqlCommand cmd = new SqlCommand(sqlstr, conn);
            if (pars != null)
            {
                for (int i = 0; i < pars.Length; i++)
                {
                    cmd.Parameters.Add(pars[i]);
                }
            }
            cmd.Parameters.Add(new SqlParameter(
                    RETURN_VALUE,
                    SqlDbType.Int,
                    4,
                    ParameterDirection.ReturnValue,
                    false,
                    0,
                    0,
                    string.Empty,
                    DataRowVersion.Default,
                    null
                )
            );
            return cmd;
        }
        private SqlDataAdapter CreateProcAdapter(string procName, SqlParameter[] pars)
        {
            //打开数据库连接
            Open();
            //数据库适配器对象
            SqlDataAdapter adapter = new SqlDataAdapter(procName, conn);
            //为存储过程
            adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (pars != null)
            {
                for (int i = 0; i < pars.Length; i++)
                {
                    adapter.SelectCommand.Parameters.Add(pars[i]);
                }
            }
            adapter.SelectCommand.Parameters.Add(new SqlParameter(
                    RETURN_VALUE,
                    SqlDbType.Int,
                    4,
                    ParameterDirection.ReturnValue,
                    false,
                    0,
                    0,
                    string.Empty,
                    DataRowVersion.Default,
                    null
                )
            );
            return adapter;
        }
        private SqlDataAdapter CreateSQLAdapter(string sqlstr, SqlParameter[] pars)
        {
            //打开数据库连接
            Open();
            //数据库适配器对象
            SqlDataAdapter adapter = new SqlDataAdapter(sqlstr, conn);
            if (pars != null)
            {
                for (int i = 0; i < pars.Length; i++)
                {
                    adapter.SelectCommand.Parameters.Add(pars[i]);
                }
            }
            adapter.SelectCommand.Parameters.Add(new SqlParameter(
                    RETURN_VALUE,
                    SqlDbType.Int,
                    4,
                    ParameterDirection.ReturnValue,
                    false,
                    0,
                    0,
                    string.Empty,
                    DataRowVersion.Default,
                    null
                )
            );
            return adapter;
        }
    }
}
