using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum UserTypeEnum
    {
        SuperAdmin = 0,
        Admin = 1,
        Officer = 2,
        User = 3
    };
}
