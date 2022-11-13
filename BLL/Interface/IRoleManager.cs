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
        List<RoleDTO> GetAllRoles();
    }
}
