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
    public class NavigationManager : INavigatorManager
    {
        private readonly INavigationNodeDal _node_navigator;
        private readonly INavigationEdgeDal _edge_navigator;
        public NavigationManager(INavigationNodeDal node_navigator, INavigationEdgeDal edge_navigator)
        {
            _node_navigator = node_navigator;
            _edge_navigator = edge_navigator;
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
        private bool InRange(double val, Tuple<double, double> lim)
        {
            return lim.Item1 <= val && val <= lim.Item2;
        }

    }
}
