using System.Collections.Generic;
using vvarscNET.Web.Client.Models;

namespace vvarscNET.Web.Client.Helper
{
    public interface IServerDataRestClient
    {
        void Add(ServerDataModel serverDataModel);
        void Delete(int id);
        IEnumerable<ServerDataModel> GetAll();
        ServerDataModel GetById(int id);
        ServerDataModel GetByIP(int ip);
        ServerDataModel GetByType(int type);
        void Update(ServerDataModel serverDataModel);
    }
}
