using BLL.Interface;
using DAL.Interface;
using DTO.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public class RoleManager : IRoleManager
    {
        private readonly IRoleDal _roleDal;
        public RoleManager(IRoleDal roleDal)
        {
            _roleDal = roleDal;
        }
        public RoleDTO AddRole(RoleDTO role)
        {
            return _roleDal.AddRole(role);
        }

        public bool DeleteRole(string id)
        {
            return _roleDal.RemoveRoleById(id);
        }

        public List<RoleDTO> GetAllRoles()
        {
            return _roleDal.GetAllRoles();
        }

        public RoleDTO UpdateRole(RoleDTO role)
        {
            return _roleDal.UpdateRole(role);
        }
    }
}
