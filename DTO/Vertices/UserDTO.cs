using DTO.Interface;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DTO.Vertices
{
    //public record struct UserPassword
    //{
    //    public UserPassword(string pwd) { this._password = pwd; }

    //    private string _password { get; set; }

    //    public override string ToString()
    //    {
    //        return "**********";
    //    }

    //    public string GetAndClear()
    //    {
    //        var to_ret = this._password;
    //        this._password = String.Empty;
    //        return to_ret;
    //    }
    //}

    //public class UserDTO
    //{
    //    public UserDTO(string Name,string Pwd)
    //    {
    //        this.UserName = Name;

    //    }
    //    public string UserName { get; set; }
    //    UserPassword UserPassword { get; init; }
    //}
    public class UserDTO : Vertex,IDynamicParse
    {
        private string _password {get; set;} = string.Empty;
        private byte[] _password_hash { get; set; } = new byte[0];
        private string _password_hash_str { get; set; } = string.Empty;

        private byte[] hash(string password, string salt)
        {
            var alg = SHA512.Create();
            return alg.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
        }

        public string UserName { get; set; } = String.Empty;

        public string Password { set {
                this._password = value;
                this._password_hash = this.hash(value, "jfasjfah3h");
                this._password_hash_str = BitConverter.ToString(this._password_hash);
            } }

        public byte[] GetPasswordHash() { return this._password_hash; }
        public string GetPasswordHashStr() { return this._password_hash_str; }
        //public string PasswordHashStr { get { return this._password_hash_str; } }
        //public byte[] PasswordHash { get { return this._password_hash; } }

        public List<RoleDTO> Roles { get; set; } = new List<RoleDTO>();

        new public bool TryParseDynamicToCurrent(dynamic? dynamicObject)
        {
            if (dynamicObject == null) return false;
            try
            {
                Id = dynamicObject["id"];
                Lable = dynamicObject["label"];
                Type = dynamicObject["type"];
                var props = dynamicObject["properties"];
                UserName = props["name"] != null ? Convert.ToString(JToken.FromObject(props["name"]).First["value"]) : string.Empty;
                _password_hash_str = props["password"] != null ? Convert.ToString(JToken.FromObject(props["password"]).First["value"]) : string.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
            return true;
        }
    }

}
