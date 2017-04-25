using vvarscNET.Core.Interfaces;
using vvarscNET.Model.ResponseModels.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Core.QueryModels.Operations
{
    public class GetClientRegistrationByAppName_Q : IQuery<ClientRegistration>
    {
        public string appName;
    }
}
