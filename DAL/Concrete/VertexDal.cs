using DAL.Interface;
using DTO;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class VertexDal : IVertexDal
    {
        private GremlinServer _server;
        public VertexDal(GremlinServer server)
        {
            _server = server;
        }

        public PathNodeDTO AddPathNode(PathNodeDTO node)
        {
            var gremlinCode = $@"
				g.addV('path')
                    .property('id', '{node.Id}')
                    .property('floor', {node.floor})
                    .property('x', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.x)})
                    .property('y', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.y)})
			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                node.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return node;
        }

        public bool DeletePathNode(PathNodeDTO node)
        {
            throw new NotImplementedException();
        }

        public bool DeletePathNodeById(string id)
        {
            throw new NotImplementedException();
        }

        public void DropAllDataFromDatabase()
        {
            var gremlinCode = $@"g.V().drop()";
            using (var gremlinClient = new GremlinClient(
                _server,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
            {
                var res = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
            }
        }

        public List<PathNodeDTO> GetAllPathNodes()
        {
            List<PathNodeDTO> res = new List<PathNodeDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('path')
			";
            using (var gremlinClient = new GremlinClient(
                            _server,
                            new GraphSON2Reader(),
                            new GraphSON2Writer(),
                            GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                foreach (var node in result)
                {
                    var curr =new PathNodeDTO();
                    curr.TryParseDynamicToCurrent(node);
                    res.Add(curr);
                }
            }
            return res;
        }

        public PathNodeDTO GetPathNodeById(string id)
        {
            PathNodeDTO res = new PathNodeDTO();

            var gremlinCode = $@"
				g.V('{id}')
			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result  = GremlinRequest.SubmitRequest(gremlinClient,gremlinCode).Result;
                res.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return res;
        }
    }
}
