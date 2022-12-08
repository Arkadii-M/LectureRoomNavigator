using DAL.Interface;
using DTO.Vertices;
using Gremlin.Net.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    public class RoleEdgeDal : IRoleEdgeDal
    {
        private static readonly string label = "has_role";
        private IGremlinClient _client;
        public RoleEdgeDal(IGremlinClient client)
        {
            _client = client;
        }
        public bool AddRoleToUser(UserDTO userDTO, RoleDTO role)
        {
            var gremlinCode = $@"
                        g.V('{userDTO.Id}')
                        .addE('{label}')
                        .to(g.V('{role.Id}'))
			";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(result.StatusAttributes);
        }

        public List<RoleDTO> GetUserRoles(UserDTO user)
        {
            List<RoleDTO> res = new List<RoleDTO>();
            var gremlinCode = $@"g.V('{user.Id}').outE('{label}').inV()";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            foreach(var role in result)
            {
                RoleDTO curr = new RoleDTO();
                curr.TryParseDynamicToCurrent(role);
                res.Add(curr);
            }
            return res;
        }

        public bool RemoveRoleFromUser(UserDTO userDTO, RoleDTO role)
        {
            //var gremlinCode = $@"g.V('{userDTO.Id}').outE('{label}').filter(inV().is('{role.Id}')).drop()";
            //var gremlinCode = $@"g.V('{userDTO.Id}').outE('{label}').inV().has('id', '{role.Id}').drop()";
            var gremlinCode = $@"g.V('{userDTO.Id}').bothE('{label}').where(otherV().hasId('{role.Id}')).drop()";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            return GremlinRequest.IsResponseOk(result.StatusAttributes);
        }
    }
}
