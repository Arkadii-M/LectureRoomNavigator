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
        
        bool LoginUser(UserDTO user);
    }
}
