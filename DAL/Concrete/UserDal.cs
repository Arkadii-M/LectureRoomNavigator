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
    public class UserDal : IUserDal
    {
        private static readonly string label = "user";
        private IGremlinClient _client;
        public UserDal(IGremlinClient client)
        {
            _client = client;
        }

        public UserDTO AddUser(UserDTO user)
        {
            var gremlinCode = $@"
				g.addV('{label}')
                    .property('id', '{user.Id}')
                    .property('name','{user.UserName}')
                    .property('password','{user.PasswordHashStr}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            user.TryParseDynamicToCurrent(result.SingleOrDefault());

            return user;
        }

        public List<UserDTO> GetAllUsers()
        {
            List<UserDTO> res = new List<UserDTO>();
            var gremlinCode = $@"
				g.V().hasLabel('{label}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;

            foreach (var user in result)
            {
                var curr = new UserDTO();
                curr.TryParseDynamicToCurrent(user);
                res.Add(curr);
            }

            return res;
        }

        public UserDTO GetUserById(string id)
        {
            UserDTO res = new UserDTO();
            var gremlinCode = $@"
				g.V('{id}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            res.TryParseDynamicToCurrent(result.SingleOrDefault());

            return res;
        }

        public UserDTO GetUserByName(string name)
        {
            UserDTO res = new UserDTO();
            var gremlinCode = $@"
				g.V().hasLabel('{label}').has('name','{name}')
			";

            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            res.TryParseDynamicToCurrent(result.SingleOrDefault());

            return res;
        }

        public bool LoginUser(UserDTO user)
        {
            UserDTO fromDb =this.GetUserByName(user.UserName);
            return (fromDb.PasswordHashStr == user.PasswordHashStr);
        }

        public UserDTO UpdateUser(UserDTO user)
        {
            var gremlinCode = $@"
				g.V('{user.Id}')
                    .property('name','{user.UserName}')
                    .property('password','{user.PasswordHashStr}')
			";
            var result = GremlinRequest.SubmitRequest(_client, gremlinCode).Result;
            user.TryParseDynamicToCurrent(result.SingleOrDefault());

            return user;
        }
    }
}
