using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Vertices;

namespace DAL.Interface
{
    public interface INavigationNodeDal
    {
        NavigationNodeDTO AddNavigationNode(NavigationNodeDTO node);

        NavigationNodeDTO GetNavigationNodeById(string id);
        List<NavigationNodeDTO> GetAllNavigationNodes();
        List<NavigationNodeDTO> GetAllNavigationNodesOnFloor(uint floor);
        NavigationNodeDTO UpdateNavigationNode(NavigationNodeDTO node);
        bool RemoveNavigationNode(NavigationNodeDTO node);
        bool RemoveNavigationNodeById(string id);

        bool RemoveAllNavigationNodesFromDatabase();
    }
}
