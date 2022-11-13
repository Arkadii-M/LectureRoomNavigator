using DAL.Interface;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class NavigationNodeDal : INavigationNodeDal
    {
        public static readonly string label = "navigation";
        private IGremlinClient _client;
        public NavigationNodeDal(IGremlinClient client)
        {
            _client = client;
        }

        public NavigationNodeDTO AddNavigationNode(NavigationNodeDTO node)
        {
            var gremlinCode = $@"
				g.addV('{label}')
                    .property('id', '{node.Id}')
                    .property('floor', {node.Floor})
                    .property('name','{node.Id}')
                    .property('x', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.X)})
                    .property('y', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.Y)})
			";


            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            node.TryParseDynamicToCurrent(result.SingleOrDefault());
            
            return node;
        }

        public bool RemoveNavigationNode(NavigationNodeDTO node)
        {
            return RemoveNavigationNodeById(node.Id);
        }

        public bool RemoveNavigationNodeById(string id)
        {
            var gremlinCode = $@"g.V('{id}').drop()";

            var res = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(res.StatusAttributes);
            
        }

        public bool RemoveAllNavigationNodesFromDatabase()
        {
            var gremlinCode = $@"g.V().hasLabel('{label}').drop()";

            var res = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(res.StatusAttributes);
            
        }

        public List<NavigationNodeDTO> GetAllNavigationNodes()
        {
            List<NavigationNodeDTO> res = new List<NavigationNodeDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('{label}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            foreach (var node in result)
            {
                var curr =new NavigationNodeDTO();
                curr.TryParseDynamicToCurrent(node);
                res.Add(curr);
            }
            
            return res;
        }

        public NavigationNodeDTO GetNavigationNodeById(string id)
        {
            NavigationNodeDTO res = new NavigationNodeDTO();

            var gremlinCode = $@"
				g.V('{id}')
			";


            var result  = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            res.TryParseDynamicToCurrent(result.SingleOrDefault());
            
            return res;
        }

        public NavigationNodeDTO UpdateNavigationNode(NavigationNodeDTO node)
        {
            var gremlinCode = $@"
				g.V('{node.Id}')
                    .property('x', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.X)})
                    .property('y', {GremlinRequest.ConvertDoubleToIntegerExpNotation(node.Y)})
			";


            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            node.TryParseDynamicToCurrent(result.SingleOrDefault());
            
            return node;
        }

        public List<NavigationNodeDTO> GetAllNavigationNodesOnFloor(uint floor)
        {
            List<NavigationNodeDTO> res = new List<NavigationNodeDTO>();
            var gremlinCode = $@"g.V().hasLabel('{label}').where('floor',{floor})";


            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            foreach (var node in result)
            {
                var curr = new NavigationNodeDTO();
                curr.TryParseDynamicToCurrent(node);
                res.Add(curr);
            }
            
            return res;
        }
    }
}
