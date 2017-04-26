using System;

namespace vvarscNET.Model.Security
{
    public class AccessToken
    {
        //Fields
        public string ID { get; set; }
        public string MemberID { get; set; }
        public string AccessTokenValue { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public int OrganizationID { get; set; }

        //Constructor
        public AccessToken()
        {

        }
    }
}
