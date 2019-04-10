using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Website
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IDataBase" in both code and config file together.
    [ServiceContract]
    public interface IDataBase
    {
        [OperationContract]
        void DoWork();
        [OperationContract]
        List<string> getUserByID(int id); 
    }
}
