using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace vvarscNET.Model.Result
{
    public class Result
    {
        public List<string> ItemIDs { get; set; }

        /// <summary>
        /// The returned Status of the request
        /// </summary>
        public HttpStatusCode Status { get; set; }

        /// <summary>
        /// Optional English description of the status that is not for showing to end users
        /// </summary>
        public string StatusDescription { get; set; }

        public Result()
        {
            ItemIDs = new List<string>();
        }
    }
}
