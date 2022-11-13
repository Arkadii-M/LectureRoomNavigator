using DTO.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO.Edges
{
    public enum NavEdgeType { straight, stairs_up,stairs_down }
    public class NavigationEdgeDTO : Edge,IDynamicParse
    {
        //public NavigationEdgeDTO() { }

        //public NavigationEdgeDTO(Edge edge):
        //    base(edge)
        //{
        //    if(edge.Properties != null)
        //    {

        //    }
        //}

        public double Distance { get; set; }
        public IMapElement? InElement { get; set; }
        public IMapElement? OutElement { get; set; }
        //public NavEdgeType EdgeType { get; set; } = NavEdgeType.straight;

        new public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;
            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                InVertexLable = dynamicObject["inVLabel"];
                OutVertexLable = dynamicObject["outVLabel"];
                InVertexId = dynamicObject["inV"];
                OutVertexId = dynamicObject["outV"];

                var props = dynamicObject["properties"];
                //Distance = Convert.ToDouble(JToken.FromObject(props["distance"]).First["value"]);
                //EdgeType = Convert.ToInt32(JToken.FromObject(props["edge_type"]).First["value"]);
                Distance = Convert.ToDouble(props["distance"]);
                //EdgeType = (NavEdgeType)Convert.ToInt32(props["edge_type"]);
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
