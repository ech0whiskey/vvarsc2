using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Model.Enums
{
    [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum UnitTypeEnum
    {
        Fleet = 1,
        Command = 2,
        Wing = 3,
        Team = 4,
        Squadron = 5,
        Platoon = 6,
        Office = 7,
        Generic = 8
    };
}
