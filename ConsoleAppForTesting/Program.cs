using DAL.Concrete;
using DTO;
using Gremlin.Net.Driver;

Console.WriteLine("Starting...");

const bool EnableSSL = false;

string Host = Environment.GetEnvironmentVariable("COSMOS_DB_HOST") ?? throw new ArgumentException("Missing env var: Host");
string PrimaryKey = Environment.GetEnvironmentVariable("COSMOS_DB_PRIMARY_KEY") ?? throw new ArgumentException("Missing env var: Host");
string Database = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME") ?? throw new ArgumentException("Missing env var: Host");
string Container = Environment.GetEnvironmentVariable("COSMOS_DB_CONTAINER_NAME") ?? throw new ArgumentException("Missing env var: Host");
int Port = Convert.ToInt32(Environment.GetEnvironmentVariable("COSMOS_DB_PORT") ?? throw new ArgumentException("Missing env var: Host"));

string containerLink = "/dbs/" + Database + "/colls/" + Container;
Console.WriteLine($"Connecting to: host: {Host}, port: {Port}, container: {containerLink}, ssl: {EnableSSL}");
var gremlinServer = new GremlinServer(Host, Port, enableSsl: EnableSSL,
                                        username: containerLink,
                                        password: PrimaryKey);

PathNodeDTO P1 = new PathNodeDTO() { Id = "P1", x = 3.35, y = 24.9, floor = 1};
PathNodeDTO P2 = new PathNodeDTO() { Id = "P2", x = 42.3, y = 32.8, floor = 2};
PathNodeDTO P3 = new PathNodeDTO() { Id = "P3", x = 5.1, y = 54, floor = 1};
PathNodeDTO P4 = new PathNodeDTO() { Id = "P4", x = 3, y = 11.2, floor = 0};

VertexDal VertDal = new VertexDal(gremlinServer);
Console.WriteLine("Drop all data from database if exist");
VertDal.DropAllDataFromDatabase();

Console.WriteLine("Add nodes");
VertDal.AddPathNode(P1);
VertDal.AddPathNode(P2);
VertDal.AddPathNode(P3);
VertDal.AddPathNode(P4);

Console.WriteLine("Try to read data");
var all = VertDal.GetAllPathNodes();

Console.WriteLine("Data from DB:\n");
foreach (var node in all)
{
    Console.WriteLine($"Id: {node.Id}\t\tx: {node.x}\t\ty: {node.y}\t\tfloor: {node.floor}");
}