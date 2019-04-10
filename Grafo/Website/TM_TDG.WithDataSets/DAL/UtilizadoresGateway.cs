using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Data.OleDb;

namespace TM_TDG.WithDataSets.DAL
{
    public class UtilizadoresGateway : BaseGateway
    {
        public UtilizadoresGateway()
        {
        }

        public DataTable GetUtilizadores()
        {
            try
            {
                DataSet ds = ExecuteQuery(GetConnection(false), "SELECT Id, Username, Nome, Email, Tipo FROM [USER]");
                return ds.Tables[0];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        public DataTable autenticar(string name, string pass)
        {
            try
            {
                SqlConnection cnx = GetConnection(true);
                DataSet ds = new DataSet();
                FillDataSet(cnx, "SELECT * FROM [USER] WHERE username='" + name + "' AND password='" + pass + "'", ds, "User");
                return ds.Tables["User"];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        public DataTable GetUtilizadoresByID(int id)
        {
            try
            {
                DataSet ds = ExecuteQuery(GetConnection(false), "SELECT * FROM [USER] where id="+id);
                return ds.Tables[0];
               
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        public DataTable GetUtilizadoresByUserNome(string id)
        {
            try
            {
                SqlConnection cnx = GetConnection(true);
                DataSet ds = new DataSet();
                FillDataSet(cnx, "SELECT * FROM [USER] WHERE username='" + id + "'", ds, "User");
                return ds.Tables["User"];
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        public DataTable GetUtilizadoresByUserName(string username)
        {
            try
            {
                DataSet ds = ExecuteQuery(GetConnection(false), "SELECT * FROM [USER] WHERE username='" + username + "'");
                return ds.Tables[0];
            }
            catch (OleDbException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }

        public void UPDATE_Utilizadores(int id, string nome, string username, string pass, string email, string tipo)
        {
            try
            {
                SqlCommand sqlcmd = new SqlCommand(
                    "UPDATE [USER] SET Nome=@nome, username=@username, Password=@pass, Email=@mail, Tipo=@tipo WHERE id=@id");
                sqlcmd.Connection = GetConnection(true);
                sqlcmd.Transaction = CurrentTransaction;

                sqlcmd.Parameters.AddWithValue("@nome", nome);
                sqlcmd.Parameters.AddWithValue("@username", username);
                sqlcmd.Parameters.AddWithValue("@pass", pass);
                sqlcmd.Parameters.AddWithValue("@mail", email);
                sqlcmd.Parameters.AddWithValue("@tipo", tipo);
                sqlcmd.Parameters.AddWithValue("@id", id);

                ExecuteNonQuery(CurrentTransaction, sqlcmd);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public int DELETE_Utilizadores(int id)
        {
            try
            {
                SqlCommand sqlcmd = new SqlCommand(
                    "DELETE FROM [USER] WHERE id=@id");
                sqlcmd.Connection = GetConnection(true);
                sqlcmd.Transaction = CurrentTransaction;

                sqlcmd.Parameters.AddWithValue("@id", id);

                return ExecuteNonQuery(CurrentTransaction, sqlcmd);
                //ExecuteQuery(GetConnection(false), "DELETE FROM Utilizador WHERE idutilizador='" + id + "'");
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public int INSERT_Utilizador(string name, string username, string pass, DateTime date, string phone, string email)
        {
            try
            {
                SqlConnection cnx = GetConnection(true);
                SqlCommand sqlcmd = new SqlCommand("INSERT INTO [USER] (username, password, name, dt_birth, phone, email, state, premium) VALUES (@username, @password, @name, @dt_birth, @phone, @email, 'Normal', 0)");
                sqlcmd.Connection = cnx;
                sqlcmd.Transaction = CurrentTransaction;

                sqlcmd.Parameters.AddWithValue("@username", username);
                sqlcmd.Parameters.AddWithValue("@password", pass);
                sqlcmd.Parameters.AddWithValue("@name", name);
                sqlcmd.Parameters.AddWithValue("@dt_birth", date);
                sqlcmd.Parameters.AddWithValue("@phone", phone);
                sqlcmd.Parameters.AddWithValue("@email", email);

                return ExecuteNonQuery(CurrentTransaction, sqlcmd);

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public void mudarPassword(string user, string pass)
        {
            try
            {
                SqlCommand sqlcmd = new SqlCommand(
                    "UPDATE [USER] SET Password=@pass WHERE username=@id");
                sqlcmd.Connection = GetConnection(true);
                sqlcmd.Transaction = CurrentTransaction;

                sqlcmd.Parameters.AddWithValue("@pass", pass);
                sqlcmd.Parameters.AddWithValue("@id", user);

                ExecuteNonQuery(CurrentTransaction, sqlcmd);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public void promoveUser(string user, string tipo)
        {
            try
            {
                if (tipo.CompareTo("user") == 0)
                        ExecuteQuery(GetConnection(false), "UPDATE [USER] SET Tipo='gestor' WHERE Username='"+user+"'");                
                if (tipo.CompareTo("gestor") == 0)
                    ExecuteQuery(GetConnection(false), "UPDATE [USER] SET Tipo='admin' WHERE Username='" + user + "'");                
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public void despromoveUser(string user, string tipo)
        {
            try{
                if (tipo.CompareTo("admin") == 0)
                    ExecuteQuery(GetConnection(false), "UPDATE [USER] SET Tipo='gestor' WHERE Username='" + user + "'");                
                if (tipo.CompareTo("gestor") == 0)
                    ExecuteQuery(GetConnection(false), "UPDATE [USER] SET Tipo='user' WHERE Username='" + user + "'");                
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public int verificaExiste(string name)
        {
            try
            {
                SqlConnection cnx = GetConnection(true);
                DataSet ds = new DataSet();
                FillDataSet(cnx, "SELECT * FROM [USER] WHERE username='" + name, ds, "User");
                string userID = "";
                foreach (DataRow theRow in ds.Tables["User"].Rows)
                {
                    userID = theRow["Id"].ToString();
                }
                return Convert.ToInt32(userID);
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public int GetIdUtilizadoresByUserName(string username)
        {
            try
            {
                DataSet ds = ExecuteQuery(GetConnection(false), "SELECT * FROM [USER] WHERE Username='" + username + "'");
                string id = "0";
                foreach (DataRow theRow in ds.Tables[0].Rows)
                {
                    id = theRow["Id"].ToString();
                }
                return Convert.ToInt32(id);
            }
            catch (OleDbException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
        public int verificar_se_existem(string nome)
        {
            try
            {
                DataSet du = ExecuteQuery(GetConnection(false), "SELECT * FROM dbo.[USER] WHERE Username='" + nome + "'");
                foreach (DataRow dbRow in du.Tables[0].Rows)
                {
                    string m = dbRow["Username"].ToString();
                    if (m.CompareTo(nome) == 0)
                        return Convert.ToInt32(dbRow["Id"].ToString());
                }
                return 0;

            }
            catch (SqlException ex)
            {
                throw new ApplicationException("Erro BD", ex);
            }
        }
    }
}