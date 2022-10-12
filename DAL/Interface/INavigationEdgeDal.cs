using DTO.Edges;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface INavigationEdgeDal
    {
        NavigationEdgeDTO AddNavigationEdge(NavigationEdgeDTO edge);

        NavigationEdgeDTO GetNavigationEdgeById(string id);
        List<NavigationEdgeDTO> GetAllNavigationEdges();
        bool RemoveNavigationEdge(NavigationEdgeDTO edge);
        bool RemoveNavigationEdgeById(string id);

        bool RemoveAllNavigationEdges();

        NavigationEdgeDTO UpdateNavigationEdge(NavigationEdgeDTO edge);

        List<NavigationEdgeDTO> GetNavigationEdgesFromNavigationNode(NavigationNodeDTO node);
    }
}
