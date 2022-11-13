using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IRoleDal
    {
        RoleDTO AddRole(RoleDTO role);
        List<RoleDTO> GetAllRoles();
        bool RemoveRole(RoleDTO role);
        bool RemoveRoleById(string id);
    }
}
