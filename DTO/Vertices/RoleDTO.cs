using DTO.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vertices
{
    public class RoleDTO : Vertex,IDynamicParse
    {
        public string Name { get; set; }
        new public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;
            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                var props = dynamicObject["properties"];
                Name = props["name"] != null ? Convert.ToString(JToken.FromObject(props["name"]).First["value"]) : string.Empty;
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
