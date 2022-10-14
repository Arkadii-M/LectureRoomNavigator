using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Edges
{
    public class Edge :IDynamicParse
    {
        //public Edge() { }
        //public Edge(Edge edge) { 
        //    Id = edge.Id;
        //    Lable = edge.Lable;
        //    Type = edge.Type;
        //    InVertexId = edge.InVertexId;
        //    OutVertexId = edge.OutVertexId;
        //    InVertexLable = edge.InVertexLable;
        //    OutVertexLable = edge.OutVertexLable;
        //}

        public string Id { get; set; } = string.Empty;
        public string? Lable { get; set; }
        public string? Type { get; set; }
        public string? InVertexLable { get; set; }
        public string? OutVertexLable { get; set; }
        public string? InVertexId { get; set; }
        public string? OutVertexId{ get; set; }
        public object? Properties { get; set; }

        public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
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
                Properties = dynamicObject["properties"];
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
