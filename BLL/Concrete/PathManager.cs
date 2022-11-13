using BLL.Interface;
using DAL.Interface;
using DTO.Interface;
using DTO.Path;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class PathManager : IPathManager
    {
        private readonly INavigationNodeDal _node_navigator;
        private readonly INavigationEdgeDal _edge_navigator;
        private readonly ILectureRoomNodeDal _lectureRoomNodeDal;
        private readonly IAlghorithmDal _alg;
        public PathManager(IAlghorithmDal alg,INavigationNodeDal node_navigator, INavigationEdgeDal edge_navigator, ILectureRoomNodeDal lectureRoomNodeDal)
        {
            _alg = alg;
            _node_navigator = node_navigator;
            _edge_navigator = edge_navigator;
            _lectureRoomNodeDal = lectureRoomNodeDal;
        }

        public List<SimplePathDTO> GetAllPathesBetweenVertices(Vertex from, Vertex to)
        {
            return _alg.FindAllPathesBetweenVertices(from, to);
        }

        public List<SimplePathDTO> GetAllPathesBetweenVertices(string from_id, string to_id)
        {
            Vertex from = new Vertex { Id = from_id};
            Vertex to = new Vertex { Id = to_id };
            return GetAllPathesBetweenVertices(from, to);
        }

        public SimplePathDTO GetOptimalPathBetweenVertices(Vertex from, Vertex to)
        {
            var weitghet_pathes = _alg.FindAllPathesWithCostBetweenVertices(from, to);
            var path = weitghet_pathes.GetPathWithMinimumCost().Item2;
            this.AttachData(ref path);
            return path;
        }

        public SimplePathDTO GetOptimalPathBetweenVertices(string from_id, string to_id)
        {
            Vertex from = new Vertex { Id = from_id };
            Vertex to = new Vertex { Id = to_id };
            return GetOptimalPathBetweenVertices(from, to);
        }

        private void AttachData(ref SimplePathDTO path)
        {
            foreach (var vert_t in path.VerticesIds)
            {
                if (vert_t.Item1 == "navigation")
                    path.NavigationNodesArray.Add(_node_navigator.GetNavigationNodeById(vert_t.Item2));
                else if (vert_t.Item1 == "lectrue_room")
                    path.LectureRoomArray.Add(_lectureRoomNodeDal.GetLectureRoomNodeById(vert_t.Item2));
                else
                    throw new Exception("Invalid path lable");
            }

            foreach (var edge_id in path.EdgesIds) // TODO: change
            {
                var edge = _edge_navigator.GetNavigationEdgeById(edge_id);

                IMapElement? in_el = path.NavigationNodesArray.Find(e => e.Id == edge.InVertexId) ?? (IMapElement)path.LectureRoomArray.Find(e => e.Id == edge.InVertexId);
                IMapElement? out_el = path.NavigationNodesArray.Find(e => e.Id == edge.OutVertexId) ?? (IMapElement)path.LectureRoomArray.Find(e => e.Id == edge.OutVertexId);

                if(in_el != null && out_el != null)
                {
                    edge.InElement = in_el;
                    edge.OutElement = out_el;
                }
                else
                    throw new Exception("Invalid path element in edge");
                path.NavigationEdgesArray.Add(edge);
            }
        }
    }
}
