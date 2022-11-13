using DTO.Interface;
using Gremlin.Net.Driver;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vertices
{
    public class NavigationNodeDTO : Vertex, IDynamicParse,IMapElement
    {
        public uint Floor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        new public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;
            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                var props = dynamicObject["properties"];
                var x_ = props["x"];
                Floor = Convert.ToUInt32(JToken.FromObject(props["floor"]).First["value"]);
                X = Convert.ToDouble(JToken.FromObject(props["x"]).First["value"]);
                Y = Convert.ToDouble(JToken.FromObject(props["y"]).First["value"]);
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
