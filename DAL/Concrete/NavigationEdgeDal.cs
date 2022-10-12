using DAL.Interface;
using DTO.Edges;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure;
using Gremlin.Net.Structure.IO.GraphSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class NavigationEdgeDal : INavigationEdgeDal
    {
        private static readonly string label = "distance";
        private GremlinServer _server;
        public NavigationEdgeDal(GremlinServer server)
        {
            _server = server;
        }
        public NavigationEdgeDTO AddNavigationEdge(NavigationEdgeDTO edge)
        {
            var gremlinCode = $@"
                        g.V('{edge.InVertexId}')
                        .addE('{label}')
                        .to(g.V('{edge.OutVertexId}'))
                        .property('id','{edge.Id}')
                        .property('distance',{GremlinRequest.ConvertDoubleToIntegerExpNotation(edge.Distance)})
                        .property('edge_type',{(int)edge.EdgeType})

			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                edge.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return edge;
        }

        public List<NavigationEdgeDTO> GetAllNavigationEdges()
        {
            List<NavigationEdgeDTO> res = new List<NavigationEdgeDTO>();
            var gremlinCode = $@"g.E().hasLabel('{label}')";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                foreach (var edge in result)
                {
                    var curr = new NavigationEdgeDTO();
                    curr.TryParseDynamicToCurrent(edge);
                    res.Add(curr);
                }
            }
            return res;
        }

        public NavigationEdgeDTO GetNavigationEdgeById(string id)
        {
            NavigationEdgeDTO edge = new NavigationEdgeDTO();
            var gremlinCode = $@"g.E('{id}')";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                edge.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return edge;
        }

        public List<NavigationEdgeDTO> GetNavigationEdgesFromNavigationNode(NavigationNodeDTO node)
        {
            List<NavigationEdgeDTO> res = new List<NavigationEdgeDTO>();
            var gremlinCode = $@"g.V('{node.Id}').bothE()";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                foreach (var edge in result)
                {
                    var curr = new NavigationEdgeDTO();
                    curr.TryParseDynamicToCurrent(edge);
                    res.Add(curr);
                }
            }
            return res;
        }

        public bool RemoveAllNavigationEdges()
        {
            var gremlinCode = $@"g.E().hasLabel('{label}').drop()";
            using (var gremlinClient = new GremlinClient(
                _server,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
            {
                var res = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                return GremlinRequest.IsResponseOk(res.StatusAttributes);
            }
        }

        public bool RemoveNavigationEdge(NavigationEdgeDTO edge)
        {
            return RemoveNavigationEdgeById(edge.Id);
        }

        public bool RemoveNavigationEdgeById(string id)
        {
            var gremlinCode = $@"g.E('{id}').drop()";
            using (var gremlinClient = new GremlinClient(
                _server,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
            {
                var res = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                return GremlinRequest.IsResponseOk(res.StatusAttributes);
            }
        }

        public NavigationEdgeDTO UpdateNavigationEdge(NavigationEdgeDTO edge)
        {
            var gremlinCode = $@"
                        g.E('{edge.Id}')
                        .property('distance',{GremlinRequest.ConvertDoubleToIntegerExpNotation(edge.Distance)})
                        .property('edge_type',{(int)edge.EdgeType})

			";

            using (var gremlinClient = new GremlinClient(
                                        _server,
                                        new GraphSON2Reader(),
                                        new GraphSON2Writer(),
                                        GremlinClient.GraphSON2MimeType))
            {
                var result = GremlinRequest.SubmitRequest(gremlinClient, gremlinCode).Result;
                edge.TryParseDynamicToCurrent(result.SingleOrDefault());
            }
            return edge;
        }
    }
}
