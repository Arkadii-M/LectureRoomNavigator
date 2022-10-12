using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAL.Interface
{
    public interface IVertexDal
    {
        PathNodeDTO AddPathNode(PathNodeDTO node);

        PathNodeDTO GetPathNodeById(string id);
        List<PathNodeDTO> GetAllPathNodes();

        bool DeletePathNode(PathNodeDTO node);
        bool DeletePathNodeById(string id);

        void DropAllDataFromDatabase();
    }
}
