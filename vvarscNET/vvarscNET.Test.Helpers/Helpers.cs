using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vvarscNET.Test.Helpers
{
    public static class Helpers
    {
        public static T Clone<T>(T obj)
        {
            string s = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(s);
        }
    }
}
