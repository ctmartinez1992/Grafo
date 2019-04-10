using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace TM_TDG.WithDataSets.DAL
{
	/// <summary>
	/// Summary description for BaseGateway.
	/// </summary>
	public abstract class BaseGateway
	{
		public BaseGateway()
		{
			//
			// TODO: Add constructor logic here
			//
		}
        //alteradas as connectionStrings Provider = SQLOLEDB;
        private const string _CONNSTR = @"Data Source=WVM010\SQLEXPRESS;Initial Catalog=Graph4Social;Persist Security Info=True;User ID=arqsi;Password=qwerty";
        private string CONNSTR
        {
            get
            {
                return _CONNSTR;
            }
        }
        private SqlTransaction myTx;

        protected SqlTransaction CurrentTransaction
        {
            get { return myTx; }
        }

        protected SqlConnection GetConnection(bool open)
        {
            SqlConnection cnx = new SqlConnection(CONNSTR);
            if (open)
                cnx.Open();
            return cnx;
        }

        protected DataSet ExecuteQuery(SqlConnection cnx, string sql)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, cnx);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        protected int ExecuteQueryID(SqlTransaction tx, SqlCommand cmd)
        {
            try
            {
            cmd.Transaction = tx;
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            int id = int.Parse(reader["ID"].ToString());

            return id;

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        protected int ExecuteNonQuery(SqlConnection cnx, string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cnx);
            return cmd.ExecuteNonQuery();
        }

        protected int ExecuteNonQuery(SqlTransaction tx, string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, tx.Connection, tx);
            return cmd.ExecuteNonQuery();
        }

        protected int ExecuteNonQuery(SqlTransaction tx, SqlCommand cmd)
        {
            cmd.Transaction = tx;
            return cmd.ExecuteNonQuery();
        }

        protected void ExecuteScalar_semreturno(SqlTransaction tx, SqlCommand cmd)
        {
            //cmd.Transaction = tx;
            cmd.ExecuteScalar();
        }

        protected int ExecuteScalar(SqlTransaction tx, SqlCommand cmd)
        {
            //cmd.Transaction = tx;
           return (Int32) cmd.ExecuteScalar();
        }


        protected void FillDataSet(SqlConnection cnx, string sql, DataSet ds, string tableName)
        {
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, cnx);
                da.Fill(ds, tableName);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public void BeginTransaction()
        {
            try
            {
                if (myTx == null)
                    myTx = GetConnection(true).BeginTransaction();
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        public void CommitTransaction()
        {
            if (myTx != null)
            {
                SqlConnection cnx = myTx.Connection;
                myTx.Commit();
                cnx.Close();
            }
        }

        public void RoolbackTransaction()
        {
            if (myTx != null)
            {
                SqlConnection cnx = myTx.Connection;
                myTx.Rollback();
                cnx.Close();
            }
        }
    }
}