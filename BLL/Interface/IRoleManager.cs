using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface
{
    public interface IRoleManager
    {
        RoleDTO AddRole(RoleDTO role);
        RoleDTO UpdateRole(RoleDTO role);
        bool DeleteRole(string id);
        List<RoleDTO> GetAllRoles();
    }
}
