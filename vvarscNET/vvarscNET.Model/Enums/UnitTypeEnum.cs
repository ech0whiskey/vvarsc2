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
        Division = 2,
        Command = 3,
        Wing = 4,
        Team = 5,
        Squadron = 6,
        Platoon = 7,
        Office = 8,
        Generic = 9
    };
}
