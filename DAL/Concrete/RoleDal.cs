using DAL.Interface;
using DTO.Vertices;
using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.Concrete
{
    public class RoleDal : IRoleDal
    {
        private static readonly string label = "role";
        private IGremlinClient _client;
        public RoleDal(IGremlinClient client)
        {
            _client = client;
        }

        public RoleDTO AddRole(RoleDTO role)
        {
            var gremlinCode = $@"
				g.addV('{label}')
                    .property('id', '{role.Id}')
                    .property('name',{role.Name})
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            role.TryParseDynamicToCurrent(result.SingleOrDefault());

            return role;
        }

        public List<RoleDTO> GetAllRoles()
        {
            List<RoleDTO> res = new List<RoleDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('{label}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;

            foreach (var role in result)
            {
                var curr = new RoleDTO();
                curr.TryParseDynamicToCurrent(role);
                res.Add(curr);
            }

            return res;
        }

        public bool RemoveRole(RoleDTO role)
        {
            return this.RemoveRoleById(role.Id);
        }

        public bool RemoveRoleById(string id)
        {
            var gremlinCode = $@"g.V('{id}').drop()";

            var res = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(res.StatusAttributes);
        }
    }
}
