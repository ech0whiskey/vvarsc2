using System.Collections.Generic;
using vvarscNET.Model.ResponseModels.People;
using System.Web;

namespace vvarscNET.Web.Client.Interfaces
{
    public interface IPeopleRestClient
    {
        IEnumerable<Rank_QRM> ListRanks(HttpContextBase Context);
    }
}
