using DTO.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Vertices
{
    public class LectureRoomDTO : Vertex, IDynamicParse, IMapElement
    {
        //public string Number { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        //public string Faculty { get; set; } = string.Empty;
        public uint NumberOfSeats { get; set; }
        public uint Floor { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public FacultyDTO? Faculty { get; set; }

        new public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;
            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                var props = dynamicObject["properties"];
                Floor = Convert.ToUInt32(JToken.FromObject(props["floor"]).First["value"]);
                //Number = Convert.ToString(JToken.FromObject(props["number"]).First["value"]);
                NumberOfSeats = Convert.ToUInt32(JToken.FromObject(props["seats"]).First["value"]);
                Name = props["name"] != null ? Convert.ToString(JToken.FromObject(props["name"]).First["value"]) : string.Empty;
                //Faculty = props["faculty"] != null ? Convert.ToString(JToken.FromObject(props["faculty"]).First["value"]) : string.Empty;
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
