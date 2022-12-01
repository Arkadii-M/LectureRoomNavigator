using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DAL.Interface
{
    public interface IUserDal
    {
        UserDTO AddUser(UserDTO user);

        UserDTO GetUserById(string id);
        UserDTO GetUserByName(string name);
        List<UserDTO> GetAllUsers();
        UserDTO UpdateUser(UserDTO user);

        bool RemoveUserById(string id);

        bool LoginUser(UserDTO user);
    }
}