using DTO.Path;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IAlghorithmDal
    {
        List<SimplePathDTO> FindAllPathesBetweenVertices(Vertex from,Vertex to);
        SimplePathWithCostDTO FindAllPathesWithCostBetweenVertices(Vertex from, Vertex to);
    }
}
