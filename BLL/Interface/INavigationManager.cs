using DTO.Edges;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface INavigationManager
    {
        // Navigation nodes
        NavigationNodeDTO AddNavigationNode(NavigationNodeDTO node);
        NavigationNodeDTO GetNavigationNodeById(string id);
        NavigationNodeDTO UpdateNavigationNode(NavigationNodeDTO node);
        bool RemoveNavigationNode(NavigationNodeDTO node);
        bool RemoveNavigationNodeById(string id);


        List<NavigationNodeDTO> GetAllNavigationNodes();
        List<NavigationNodeDTO> GetAllNavigationNodesOnFloor(uint floor);
        List<NavigationNodeDTO> GetAllNavigationNodesOnFloorWithLimitOfCoordinates(
            uint floor,
            Tuple<double, double> x_lim,
            Tuple<double, double> y_lim);

        // Navigation Edges
        NavigationEdgeDTO AddNavigationEdge(NavigationEdgeDTO edge);

        NavigationEdgeDTO GetNavigationEdgeById(string id);

        List<NavigationEdgeDTO> GetAllNavigationEdges();
        bool RemoveNavigationEdge(NavigationEdgeDTO edge);
        bool RemoveNavigationEdgeById(string id);

        NavigationEdgeDTO UpdateNavigationEdge(NavigationEdgeDTO edge);

        (NavigationNodeDTO, List<NavigationEdgeDTO>) GetNavigationNodeAndHisEdges(string Id);

    }
}
