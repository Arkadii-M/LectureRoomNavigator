using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRoleEdgeDal
    {
        bool AddRoleToUser(UserDTO userDTO,RoleDTO role);
        bool RemoveRoleFromUser(UserDTO userDTO, RoleDTO role);
        List<RoleDTO> GetUserRoles(UserDTO user);
    }
}
