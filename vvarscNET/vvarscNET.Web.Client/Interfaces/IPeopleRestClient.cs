using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.People;
using System.Web;

namespace vvarscNET.Web.Client.Interfaces
{
    public interface IPeopleRestClient
    {
        IEnumerable<ListRanks_QRM> ListRanks(HttpContextBase Context);
    }
}
