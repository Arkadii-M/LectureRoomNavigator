using DAL.Interface;
using DTO.Path;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class AlghorithmDal : IAlghorithmDal
    {
        private IGremlinClient _client;
        public AlghorithmDal(IGremlinClient client)
        {
            _client = client;
        }

        public List<SimplePathDTO> FindAllPathesBetweenVertices(Vertex from, Vertex to)
        {
            List<SimplePathDTO> all_pathes = new List<SimplePathDTO>();

            var gremlinCode = $@"g.V('{from.Id}')
                                .repeat(bothE().otherV().simplePath())
                                .until(hasId('{to.Id}'))
                                .path()";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            foreach (var path in result)
            {
                var curr = new SimplePathDTO();
                curr.TryParseDynamicToCurrent(path);
                all_pathes.Add(curr);
            }
            return all_pathes;
        }

        public SimplePathWithCostDTO FindAllPathesWithCostBetweenVertices(Vertex from, Vertex to)
        {
            SimplePathWithCostDTO path_with_cost =new  SimplePathWithCostDTO();

            var gremlinCode = $@"
                                g.V('{from.Id}')
                                .repeat(bothE().otherV().simplePath())
                                .until(has('id', '{to.Id}'))
                                .path().as('path')
                                .map(
                                    unfold()
                                    .coalesce(values('distance'),constant(0)).sum())
                                .as('cost')
                                .select('cost','path')";


            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            foreach (var weighted_path in result)
            {
                var curr = new SimplePathDTO();
                path_with_cost.TryParseDynamicToCurrent(weighted_path);
            }
            return path_with_cost;
            
        }
    }
}
