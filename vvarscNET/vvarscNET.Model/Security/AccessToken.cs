using System;

namespace vvarscNET.Model.Security
{
    public class AccessToken
    {
        //Fields
        public DateTime expiration { get; set; }
        public DateTime expiration_offline { get; set; }
        public string id { get; set; }
        public string member_language { get; set; }
        public string member_pid { get; set; }
        public string member_private_key { get; set; }
        public string member_region { get; set; }
        public string member_timezone { get; set; }
        public string member_uri { get; set; }
        public string token_type { get; set; }
        public string application_pid { get; set; }

        //Constructor
        public AccessToken()
        {

        }
    }
}
