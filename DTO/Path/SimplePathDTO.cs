using DTO.Edges;
using DTO.Vertices;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Path
{
    public class SimplePathDTO : IDynamicParse
    {
        public List<DTO.Vertices.Vertex> VertexArray { get; set; } = new List<DTO.Vertices.Vertex>();
        public List<DTO.Edges.Edge> EdgeArray { get; set; } = new List<Edge>();
        public bool IsAnyPathExists => VertexArray.Any() && EdgeArray.Any();

        public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;

            try
            {
                var vertices_and_adges = dynamicObject["objects"];

                foreach (var v_e in vertices_and_adges)
                {
                    if (v_e["type"] == "vertex")
                    {
                        DTO.Vertices.Vertex vert = new DTO.Vertices.Vertex();
                        vert.TryParseDynamicToCurrent(v_e);
                        VertexArray.Add(vert);
                    }
                    else if(v_e["type"] == "edge")
                    {
                        DTO.Edges.Edge edge = new DTO.Edges.Edge();
                        edge.TryParseDynamicToCurrent(v_e);
                        EdgeArray.Add(edge);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }
}
