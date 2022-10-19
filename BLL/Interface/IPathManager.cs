using DTO.Path;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IPathManager
    {
        SimplePathDTO GetOptimalPathBetweenVertices(Vertex from, Vertex to);
        List<SimplePathDTO> GetAllPathesBetweenVertices(Vertex from, Vertex to);

        SimplePathDTO GetOptimalPathBetweenVertices(string from_id, string to_id);
        List<SimplePathDTO> GetAllPathesBetweenVertices(string from_id, string to_id);
    }
}
