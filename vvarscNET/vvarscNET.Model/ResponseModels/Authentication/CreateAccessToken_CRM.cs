using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.ResponseModels.Authentication
{
    public class CreateAccessToken_CRM
    {
        public int ID;
        public Guid AccessToken;
        public DateTime ValidFrom;
        public DateTime ValidTo;
    }
}
