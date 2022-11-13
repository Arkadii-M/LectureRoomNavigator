using DTO.Edges;
using DTO.Interface;
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

        public List<Tuple<string,string>> VerticesIds { get; set; } = new List<Tuple<string, string>>();
        public List<string> EdgesIds { get; set; } = new List<string>();

        public List<DTO.Vertices.NavigationNodeDTO> NavigationNodesArray { get; set; } = new List<NavigationNodeDTO>();
        public List<DTO.Vertices.LectureRoomDTO> LectureRoomArray { get; set; } = new List<LectureRoomDTO>();
        public List<DTO.Edges.NavigationEdgeDTO> NavigationEdgesArray { get; set; } = new List<NavigationEdgeDTO>();


        public bool IsAnyPathExists => VerticesIds.Any() && EdgesIds.Any();

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
                        VerticesIds.Add(new Tuple<string, string>(v_e["label"], v_e["id"]));
                    }
                    else if (v_e["type"] == "edge")
                    {
                        EdgesIds.Add(v_e["id"]);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }

        //public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        //{
        //    if (dynamicObject == null) return false;

        //    try
        //    {
        //        var vertices_and_adges = dynamicObject["objects"];

        //        foreach (var v_e in vertices_and_adges)
        //        {
        //            if (v_e["type"] == "vertex")
        //            {
        //                DTO.Vertices.Vertex vert = new DTO.Vertices.Vertex();
        //                vert.TryParseDynamicToCurrent(v_e);
        //                VertexArray.Add(vert);
        //            }
        //            else if(v_e["type"] == "edge")
        //            {
        //                DTO.Edges.Edge edge = new DTO.Edges.Edge();
        //                edge.TryParseDynamicToCurrent(v_e);
        //                EdgeArray.Add(edge);
        //            }
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        Console.WriteLine(e.ToString());
        //        return false;
        //    }
        //    return true;
        //}
    }
}
