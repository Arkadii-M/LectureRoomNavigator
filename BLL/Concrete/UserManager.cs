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
    public class UserManager : IUserManager
    {
        private readonly IUserDal _userDal;
        private readonly IRoleEdgeDal _roleEdgeDal;
        public UserManager(IUserDal userDal, IRoleEdgeDal roleEdgeDal)
        {
            _userDal = userDal;
            _roleEdgeDal = roleEdgeDal;
        }
        public UserDTO AddUser(UserDTO user)
        {
            return _userDal.AddUser(user);
        }

        public List<UserDTO> GetAllUsers()
        {
            var users = _userDal.GetAllUsers();
            users.ForEach(user => { this.AttachUserRoles(ref user); });
            return users;
        }

        public UserDTO GetUser(string username)
        {
            var user = _userDal.GetUserByName(username);
            this.AttachUserRoles(ref user);
            return user;
        }

        public List<RoleDTO> GetUserRoles(string username)
        {
            return this.GetUser(username).Roles;
        }

        public bool LoginUser(UserDTO user)
        {
            return _userDal.LoginUser(user);
        }

        private void AttachUserRoles(ref UserDTO user)
        {
            user.Roles = _roleEdgeDal.GetUserRoles(user);
        }
    }
}
