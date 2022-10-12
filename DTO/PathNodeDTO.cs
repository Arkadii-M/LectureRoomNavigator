using Gremlin.Net.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class PathNodeDTO : Vertex, IDynamicParse
    {
        public uint floor { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if(dynamicObject == null) return false;
            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                var props = dynamicObject["properties"];
                floor = Convert.ToUInt32(JToken.FromObject(props["floor"]).First["value"]);
                x = Convert.ToDouble(JToken.FromObject(props["x"]).First["value"]);
                y = Convert.ToDouble(JToken.FromObject(props["y"]).First["value"]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
}
