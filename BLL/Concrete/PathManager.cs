using BLL.Interface;
using DAL.Interface;
using DTO.Path;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class PathManager : IPathManager
    {
        private readonly INavigationNodeDal _node_navigator;
        private readonly INavigationEdgeDal _edge_navigator;
        private readonly IAlghorithmDal _alg;
        public PathManager(IAlghorithmDal alg,INavigationNodeDal node_navigator, INavigationEdgeDal edge_navigator)
        {
            _alg = alg;
            _node_navigator = node_navigator;
            _edge_navigator = edge_navigator;
        }

        public List<SimplePathDTO> GetAllPathesBetweenVertices(Vertex from, Vertex to)
        {
            return _alg.FindAllPathesBetweenVertices(from, to);
        }

        public List<SimplePathDTO> GetAllPathesBetweenVertices(string from_id, string to_id)
        {
            Vertex from = new Vertex { Id = from_id};
            Vertex to = new Vertex { Id = to_id };
            return GetAllPathesBetweenVertices(from, to);
        }

        public SimplePathDTO GetOptimalPathBetweenVertices(Vertex from, Vertex to)
        {
            var weitghet_pathes = _alg.FindAllPathesWithCostBetweenVertices(from, to);
            if(weitghet_pathes.IsAnyPathExists)
                return weitghet_pathes.GetPathWithMinimumCost().Item2;
            throw new Exception("No path between verticex exists");
        }

        public SimplePathDTO GetOptimalPathBetweenVertices(string from_id, string to_id)
        {
            Vertex from = new Vertex { Id = from_id };
            Vertex to = new Vertex { Id = to_id };
            return GetOptimalPathBetweenVertices(from, to);
        }
    }
}
