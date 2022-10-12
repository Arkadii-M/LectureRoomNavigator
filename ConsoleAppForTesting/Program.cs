using DAL.Concrete;
using DAL.Interface;
using DTO.Edges;
using DTO.Vertices;
using Gremlin.Net.Driver;

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

NavigationNodeDTO P1 = new NavigationNodeDTO() { Id = "P1", X = 3.35, Y = 24.9, Floor = 1};
NavigationNodeDTO P2 = new NavigationNodeDTO() { Id = "P2", X = 42.3, Y = 32.8, Floor = 2};
NavigationNodeDTO P3 = new NavigationNodeDTO() { Id = "P3", X = 5.1, Y = 54, Floor = 1};
NavigationNodeDTO P4 = new NavigationNodeDTO() { Id = "P4", X = 3, Y = 11.2, Floor = 0};
NavigationNodeDTO P5 = new NavigationNodeDTO() { Id = "P5", X = 0, Y = 0, Floor = 0 };

NavigationEdgeDTO P1_P2 = new NavigationEdgeDTO() { 
    Id = "P1_P2",
    InVertexId = P1.Id,
    OutVertexId = P2.Id,
    EdgeType = NavEdgeType.straight,
    Distance = 3.5 };

NavigationEdgeDTO P2_P3 = new NavigationEdgeDTO()
{
    Id = "P2_P3",
    InVertexId = P2.Id,
    OutVertexId = P3.Id,
    EdgeType = NavEdgeType.straight,
    Distance = 8.11
};

INavigationNodeDal VertDal = new NavigationNodeDal(gremlinServer);
INavigationEdgeDal EdgeDal = new NavigationEdgeDal(gremlinServer);

Console.Write("Drop all navigation nodes from database if exist...");
if (VertDal.RemoveAllNavigationNodesFromDatabase()) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

Console.Write("Drop all navigation edges from database if exist...");
if (EdgeDal.RemoveAllNavigationEdges()) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

Console.WriteLine("Add nodes");
VertDal.AddNavigationNode(P1);
VertDal.AddNavigationNode(P2);
VertDal.AddNavigationNode(P3);
VertDal.AddNavigationNode(P4);
VertDal.AddNavigationNode(P5);

Console.Write("Try to update node with id = 'P5'...");
P5.X = 3.51;
P5 = VertDal.UpdateNavigationNode(P5);
if(double.Equals(3.51,P5.X)) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

Console.Write("Try to delete node with id = 'P5'...");
if( VertDal.RemoveNavigationNode(P5)) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

Console.WriteLine("Add edges");
EdgeDal.AddNavigationEdge(P1_P2);
EdgeDal.AddNavigationEdge(P2_P3);


Console.WriteLine("Try to read vertices");
var all_v = VertDal.GetAllNavigationNodes();

Console.WriteLine("Vertices:\n");
foreach (var node in all_v)
{
    Console.WriteLine($"Id: {node.Id}\t\tx: {node.X}\t\ty: {node.Y}\t\tfloor: {node.Floor}");
}



Console.WriteLine("Try to read edges");
var all_e = EdgeDal.GetAllNavigationEdges();

Console.WriteLine("Edges:\n");
foreach (var edge in all_e)
{
    Console.WriteLine($"Id: {edge.Id}\t\tinV: {edge.InVertexId}\t\toutV: {edge.OutVertexId}\t\tdistance: {edge.Distance}");
}

Console.WriteLine("Try to read edges for specific vertex:");
var p2_edges = EdgeDal.GetNavigationEdgesFromNavigationNode(P2);

Console.WriteLine("Edges from P2:\n");
foreach (var edge in p2_edges)
{
    Console.WriteLine($"Id: {edge.Id}\t\tinV: {edge.InVertexId}\t\toutV: {edge.OutVertexId}\t\tdistance: {edge.Distance}");
}