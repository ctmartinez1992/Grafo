using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace TM_TDG.WithDataSets.BLL
{
    public class Utilizadores
    {
        public Utilizadores()
        {
        }

        public DataTable GetUtilizadores()
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            return dal.GetUtilizadores();
        }
        public DataTable Autenticar(string Nome, string Pass)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            return dal.autenticar(Nome, Pass);
        }
        public DataTable GetUtilizadoresByUserNome(string Nome)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            return dal.GetUtilizadoresByUserNome(Nome);
        }
        public DataTable GetUtilizadoresByID(int id)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            return dal.GetUtilizadoresByID(id);
        }
        public void UPDATE_Utilizadores(int id, string Nome, string username, string Pass, string email, string tipo)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            dal.UPDATE_Utilizadores(id, Nome, username, Pass, email, tipo);
        }
        public int DELETE_Utilizadores(int id)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            return dal.DELETE_Utilizadores(id);
        }
        public int INSERT_Utilizador(string name, string username, string pass, DateTime date, string phone, string email)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            if ((int)dal.GetUtilizadoresByUserName(username).Rows.Count == 0)
                return dal.INSERT_Utilizador( name,  username,  pass,  date,  phone,  email);
            else
                return 0;
        }
        public void mudarPassword(string user, string pass)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            dal.mudarPassword(user,pass);
        }
        public void promoveUser(string user, string tipo)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            dal.promoveUser(user, tipo);
        }
        public void despromoveUser(string user, string tipo)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            dal.despromoveUser(user, tipo);
        }
        public int verificar_se_exite(string nome)
        {
            DAL.UtilizadoresGateway dal = new TM_TDG.WithDataSets.DAL.UtilizadoresGateway();
            return dal.verificar_se_existem(nome);
        }
    }
}
