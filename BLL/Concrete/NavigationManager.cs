using BLL.Interface;
using DAL.Interface;
using DTO.Edges;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLL.Concrete
{
    public class NavigationManager : INavigationManager
    {
        private readonly INavigationNodeDal _node_navigator;
        private readonly INavigationEdgeDal _edge_navigator;
        private readonly ILectureRoomNodeDal _lectureRoomNodeDal;
        public NavigationManager(INavigationNodeDal node_navigator, INavigationEdgeDal edge_navigator, ILectureRoomNodeDal lectureRoomNodeDal)
        {
            _node_navigator = node_navigator;
            _edge_navigator = edge_navigator;
            _lectureRoomNodeDal = lectureRoomNodeDal;
        }

        public NavigationNodeDTO AddNavigationNode(NavigationNodeDTO node)
        {
            node.Id = Guid.NewGuid().ToString();
            return _node_navigator.AddNavigationNode(node);
        }

        public bool RemoveNavigationNode(NavigationNodeDTO node)
        {
            return _node_navigator.RemoveNavigationNode(node);
        }

        public bool RemoveNavigationNodeById(string id)
        {
            return _node_navigator.RemoveNavigationNodeById(id);
        }

        public List<NavigationNodeDTO> GetAllNavigationNodes()
        {
            return _node_navigator.GetAllNavigationNodes();
        }

        public List<NavigationNodeDTO> GetAllNavigationNodesOnFloor(uint floor)
        {
            return _node_navigator.GetAllNavigationNodesOnFloor(floor);
        }

        public List<NavigationNodeDTO> GetAllNavigationNodesOnFloorWithLimitOfCoordinates(uint floor, Tuple<double, double> x_lim, Tuple<double, double> y_lim)
        {
            return this.GetAllNavigationNodesOnFloor(floor).Where(filter =>
                                                                    InRange(filter.X, x_lim) &&
                                                                    InRange(filter.Y, y_lim)).ToList();
        }

        public (NavigationNodeDTO, List<NavigationEdgeDTO>) GetNavigationNodeAndHisEdges(string Id, bool attach_elemnts = false)
        {
            NavigationNodeDTO node = _node_navigator.GetNavigationNodeById(Id);
            List<NavigationEdgeDTO> edges = _edge_navigator.GetNavigationEdgesFromNavigationNode(node);
            if (attach_elemnts)
                edges.ForEach(el => { this.AttachMapElements(ref el); });
            return (node, edges);
        }

        public NavigationNodeDTO GetNavigationNodeById(string id)
        {
            return _node_navigator.GetNavigationNodeById(id);
        }

        public NavigationNodeDTO UpdateNavigationNode(NavigationNodeDTO node)
        {
            return _node_navigator.UpdateNavigationNode(node);
        }

        private bool InRange(double val, Tuple<double, double> lim)
        {
            return lim.Item1 <= val && val <= lim.Item2;
        }

        public NavigationEdgeDTO AddNavigationEdge(NavigationEdgeDTO edge)
        {
            edge.Id = Guid.NewGuid().ToString();
            return _edge_navigator.AddNavigationEdge(edge);
        }

        public NavigationEdgeDTO GetNavigationEdgeById(string id, bool attach_elemnts = false)
        {
            var edge = _edge_navigator.GetNavigationEdgeById(id);
            if (attach_elemnts)
                this.AttachMapElements(ref edge);
            return edge;
        }

        public List<NavigationEdgeDTO> GetAllNavigationEdges(bool attach_elemnts = false)
        {
            var edges = _edge_navigator.GetAllNavigationEdges();
            if (attach_elemnts)
                edges.ForEach(el => { this.AttachMapElements(ref el); });

            return edges;
        }

        public bool RemoveNavigationEdge(NavigationEdgeDTO edge)
        {
            return _edge_navigator.RemoveNavigationEdge(edge);
        }

        public bool RemoveNavigationEdgeById(string id)
        {
            return _edge_navigator.RemoveNavigationEdgeById(id);
        }

        public NavigationEdgeDTO UpdateNavigationEdge(NavigationEdgeDTO edge)
        {
            return _edge_navigator.UpdateNavigationEdge(edge);
        }
        private void AttachMapElements(ref NavigationEdgeDTO edge)
        {
            if(edge.InVertexId is not null)
            {
                if (edge.InVertexLable == "lectrue_room") { edge.InElement = this._lectureRoomNodeDal.GetLectureRoomNodeById(edge.InVertexId); }
                else if (edge.InVertexLable == "navigation") { edge.InElement = this._node_navigator.GetNavigationNodeById(edge.InVertexId); }
            }
            if (edge.OutVertexId is not null)
            {
                if (edge.OutVertexLable == "lectrue_room") { edge.OutElement = this._lectureRoomNodeDal.GetLectureRoomNodeById(edge.OutVertexId); }
                else if (edge.OutVertexLable == "navigation") { edge.OutElement = this._node_navigator.GetNavigationNodeById(edge.OutVertexId); }
            }

        }

        public List<NavigationNodeDTO> GetNavigationNodesByIds(string[] ids)
        {
            List<NavigationNodeDTO> res = new List<NavigationNodeDTO>();
            foreach (string id in ids)
                res.Add(_node_navigator.GetNavigationNodeById(id));
            return res;
        }

        public List<NavigationEdgeDTO> GetNavigationEdgesByIds(string[] ids, bool attach_elemnts = true)
        {
            List<NavigationEdgeDTO> res = new List<NavigationEdgeDTO>();
            foreach (string id in ids)
                res.Add(_edge_navigator.GetNavigationEdgeById(id));
            if (attach_elemnts)
                res.ForEach(el => { this.AttachMapElements(ref el); });
            return res;
        }

        public NavigationNodeDTO GetEnterNode()
        {
            return _node_navigator.GetNavigationEnterNode();
        }
    }
}
