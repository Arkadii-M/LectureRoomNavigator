using BLL.Interface;
using DAL.Interface;
using DTO.Edges;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class NavigationManager : INavigationManager
    {
        private readonly INavigationNodeDal _node_navigator;
        private readonly INavigationEdgeDal _edge_navigator;
        public NavigationManager(INavigationNodeDal node_navigator, INavigationEdgeDal edge_navigator)
        {
            _node_navigator = node_navigator;
            _edge_navigator = edge_navigator;
        }

        public NavigationNodeDTO AddNavigationNode(NavigationNodeDTO node)
        {
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

        public (NavigationNodeDTO, List<NavigationEdgeDTO>) GetNavigationNodeAndHisEdges(string Id)
        {
            NavigationNodeDTO node = _node_navigator.GetNavigationNodeById(Id);
            List<NavigationEdgeDTO> edges = _edge_navigator.GetNavigationEdgesFromNavigationNode(node);
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
            return _edge_navigator.AddNavigationEdge(edge);
        }

        public NavigationEdgeDTO GetNavigationEdgeById(string id)
        {
            return _edge_navigator.GetNavigationEdgeById(id);
        }

        public List<NavigationEdgeDTO> GetAllNavigationEdges()
        {
            return _edge_navigator.GetAllNavigationEdges();
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
    }
}
