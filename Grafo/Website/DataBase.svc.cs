using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Website
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DataBase" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DataBase.svc or DataBase.svc.cs at the Solution Explorer and start debugging.
    public class DataBase : IDataBase
    {
        public void DoWork()
        {
        }
        public List<string> getUserByID(int id)
        {
            //TM_TDG.WithDataSets.BLL.Utilizadores userBLL = new TM_TDG.WithDataSets.BLL.Utilizadores();
            //DataTable dtUser = userBLL.GetUtilizadoresByID(id);
            //DataRow dr = dtUser.Rows[0];
            List<string> user=new List<string>();
            user.Insert(0, "1");//id
            user.Insert(1, "nuno");//nome
            user.Insert(2, "24/03/1992");//data nascimento
            user.Insert(3, "teste@gmail.com");//email
            //user.Insert(0,dr.ItemArray[0].ToString());//id
            //user.Insert(1, dr.ItemArray[3].ToString());//nome
            //user.Insert(2, dr.ItemArray[4].ToString());//data nascimento
            //user.Insert(3, dr.ItemArray[6].ToString());//email
            return user;
        }
    }
}
