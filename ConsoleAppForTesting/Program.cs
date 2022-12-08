using DAL.Concrete;
using DAL.Interface;
using DTO;
using DTO.Edges;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System.Xml.Linq;

Console.WriteLine("Starting...");

const bool EnableSSL = false;

string Host = Environment.GetEnvironmentVariable("COSMOS_DB_HOST") ?? throw new ArgumentException("Missing env var: Host");
string PrimaryKey = Environment.GetEnvironmentVariable("COSMOS_DB_PRIMARY_KEY") ?? throw new ArgumentException("Missing env var: Primary key");
string Database = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME") ?? throw new ArgumentException("Missing env var: Database name");
string Container = Environment.GetEnvironmentVariable("COSMOS_DB_CONTAINER_NAME") ?? throw new ArgumentException("Missing env var: Container name");
int Port = Convert.ToInt32(Environment.GetEnvironmentVariable("COSMOS_DB_PORT") ?? throw new ArgumentException("Missing env var: Port"));

string containerLink = "/dbs/" + Database + "/colls/" + Container;
Console.WriteLine($"Connecting to: host: {Host}, port: {Port}, container: {containerLink}, ssl: {EnableSSL}");
var gremlinServer = new GremlinServer(Host, Port, enableSsl: EnableSSL,
                                        username: containerLink,
                                        password: PrimaryKey);


using (var gremlinClient = new GremlinClient(
                gremlinServer,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
{

    IRoleDal role_dal = new RoleDal(gremlinClient);
    var admin_role = new RoleDTO() { Id = "admin_role_id", Name = "admin" };
    var user_role = new RoleDTO() { Id = "user_role_id", Name = "user" };

    try { role_dal.AddRole(admin_role); } catch (Exception) { }
    try { role_dal.AddRole(user_role); } catch (Exception) { }

    UserDTO admin = new UserDTO() { Id = "admin_id", UserName = "admin", Password = "admin" };
    UserDTO user = new UserDTO() { Id = "user_id", UserName = "user", Password = "user" };

    IUserDal user_dal = new UserDal(gremlinClient);
    try { user_dal.AddUser(admin); } catch(Exception) { }
    try { user_dal.AddUser(user); } catch (Exception) { }

    IRoleEdgeDal edge_dal = new RoleEdgeDal(gremlinClient);
    try { edge_dal.AddRoleToUser(admin, user_role); } catch (Exception) { }
    try { edge_dal.AddRoleToUser(admin, admin_role); } catch (Exception) { }

    try { edge_dal.AddRoleToUser(user, user_role); } catch (Exception) { }



    FacultyDTO AppliedMath = new FacultyDTO { Id = new Guid().ToString(), Name = "Applied mathematics and informatics" };
    IFacultyDal facultyDal = new FacultyDal(gremlinClient);

    try { facultyDal.AddFaculty(AppliedMath); } catch (Exception) { }

}


Console.WriteLine("Done!");