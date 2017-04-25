using System;
using System.Collections.Generic;

namespace vvarscNET.Model.Security
{
    public class JwtApiToken
    {
        public string TokenID { get; set; }
        public string MemberID { get; set; }
        public string App { get; set; }
        public long Exp { get; set; }
        public long Nbf { get; set; }

        public IDictionary<string, object> ToPayload()
        {
            var payload = new Dictionary<string, object>
            {
                ["TokenID"] = TokenID,
                ["MemberID"] = MemberID,
                ["App"] = App,
                ["exp"] = Exp,
                ["nbf"] = Nbf
            };
            return payload;
        }

        public static JwtApiToken FromPayload(IDictionary<string, object> payload)
        {
            return new JwtApiToken
            {
                TokenID = payload["TokenID"].ToString(),
                MemberID = payload["MemberID"].ToString(),
                App = payload["App"].ToString(),
                Exp = Convert.ToInt64(payload["exp"]),
                Nbf = Convert.ToInt64(payload["nbf"])
            };
        }
    }
}
