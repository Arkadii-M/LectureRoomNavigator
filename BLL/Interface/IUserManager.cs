using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IUserManager
    {
        UserDTO AddUser(UserDTO user);
        List<UserDTO> GetAllUsers();
        UserDTO GetUser(string username);
        UserDTO GetUserById(string username);
        List<RoleDTO> GetUserRoles(string username);
        UserDTO UpdateUser(UserDTO user);
        bool RemoveUserById(string id);

        bool LoginUser(UserDTO user);
    }
}
