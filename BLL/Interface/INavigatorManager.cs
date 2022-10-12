using DTO.Edges;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface INavigatorManager
    {
        List<NavigationNodeDTO> GetAllNavigationNodes();
        List<NavigationNodeDTO> GetAllNavigationNodesOnFloor(uint floor);
        List<NavigationNodeDTO> GetAllNavigationNodesOnFloorWithLimitOfCoordinates(
            uint floor,
            Tuple<double, double> x_lim,
            Tuple<double, double> y_lim);
        (NavigationNodeDTO, List<NavigationEdgeDTO>) GetNavigationNodeAndHisEdges(string Id);

    }
}
