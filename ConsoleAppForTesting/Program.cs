using DAL.Concrete;
using DAL.Interface;
using DTO;
using DTO.Edges;
using DTO.Vertices;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using System.Xml.Linq;

Console.WriteLine("Starting...");

//var user = new UserDTO("user", "pwd");

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



//using (var gremlinClient = new GremlinClient(
//                gremlinServer,
//                new GraphSON2Reader(),
//                new GraphSON2Writer(),
//                GremlinClient.GraphSON2MimeType))
//{
//    LectureRoomDTO Room1 = new() { Id = "Room1", Floor = 1, Name = "111", Faculty = "Faculty1", NumberOfSeats = 60, X = 1254.501875, Y = 502.940478 };
//    LectureRoomDTO Room2 = new() { Id = "Room2", Floor = 1, Name = "112", Faculty = "Faculty2", NumberOfSeats = 30, X = 1356.318469, Y = 613.092833 };


//    NavigationNodeDTO P1 = new() { Id = "P1", X = 1733.408435526993, Y = 351.583569697352, Floor = 1 };
//    NavigationNodeDTO P2 = new() { Id = "P2", X = 1725.5514825053524, Y = 465.50938851113926, Floor = 1 };
//    NavigationNodeDTO P3 = new() { Id = "P3", X = 1537.9667291136855, Y = 479.2590562990102, Floor = 1 };
//    NavigationNodeDTO P4 = new() { Id = "P4", X = 3, Y = 11.2, Floor = 0 };
//    NavigationNodeDTO P5 = new() { Id = "P5", X = 0, Y = 0, Floor = 0 };

//    NavigationEdgeDTO P1_P2 = new()
//    {
//        Id = "P1_P2",
//        InVertexId = P1.Id,
//        OutVertexId = P2.Id,
//        //EdgeType = NavEdgeType.straight,
//        Distance = 3.5
//    };

//    NavigationEdgeDTO P2_P3 = new()
//    {
//        Id = "P2_P3",
//        InVertexId = P2.Id,
//        OutVertexId = P3.Id,
//        //EdgeType = NavEdgeType.straight,
//        Distance = 8.11
//    };

//    NavigationEdgeDTO P3_Room1 = new() { Id = "P3_Room1",InVertexId = P3.Id,OutVertexId = Room1.Id,Distance = 3.1};

//    INavigationNodeDal VertDal = new NavigationNodeDal(gremlinClient);
//    INavigationEdgeDal EdgeDal = new NavigationEdgeDal(gremlinClient);
//    ILectureRoomNodeDal RoomDal = new LectureRoomDal(gremlinClient);

//    Console.Write("Drop all navigation nodes from database if exist...");
//    if (VertDal.RemoveAllNavigationNodesFromDatabase()) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

//    Console.Write("Drop all navigation edges from database if exist...");
//    if (EdgeDal.RemoveAllNavigationEdges()) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

//    Console.Write("Drop all lecture room nodes from database if exist...");
//    if (RoomDal.RemoveAllLectureRoomNodesFromDatabase()) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

//    Console.WriteLine("Add nodes");
//    VertDal.AddNavigationNode(P1);
//    VertDal.AddNavigationNode(P2);
//    VertDal.AddNavigationNode(P3);
//    VertDal.AddNavigationNode(P4);
//    VertDal.AddNavigationNode(P5);

//    Console.Write("Add lecture rooms...");
//    RoomDal.AddLectureRoomNode(Room1);
//    RoomDal.AddLectureRoomNode(Room2);

//    Console.Write("Try to update node with id = 'P5'...");
//    P5.X = 3.51;
//    P5 = VertDal.UpdateNavigationNode(P5);
//    if (double.Equals(3.51, P5.X)) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

//    Console.Write("Try to delete node with id = 'P5'...");
//    if (VertDal.RemoveNavigationNode(P5)) Console.WriteLine("  OK"); else Console.WriteLine(" Fail");

//    Console.WriteLine("Add edges");
//    EdgeDal.AddNavigationEdge(P1_P2);
//    EdgeDal.AddNavigationEdge(P2_P3);
//    EdgeDal.AddNavigationEdge(P3_Room1);

//    Console.WriteLine("Try to read vertices");
//    var all_v = VertDal.GetAllNavigationNodes();

//    Console.WriteLine("Vertices:\n");
//    foreach (var node in all_v)
//    {
//        Console.WriteLine($"Id: {node.Id}\t\tx: {node.X}\t\ty: {node.Y}\t\tfloor: {node.Floor}");
//    }



//    Console.WriteLine("Try to read edges");
//    var all_e = EdgeDal.GetAllNavigationEdges();

//    Console.WriteLine("Edges:\n");
//    foreach (var edge in all_e)
//    {
//        Console.WriteLine($"Id: {edge.Id}\t\tinV: {edge.InVertexId}\t\toutV: {edge.OutVertexId}\t\tdistance: {edge.Distance}");
//    }

//    Console.WriteLine("Try to read edges for specific vertex:");
//    var p2_edges = EdgeDal.GetNavigationEdgesFromNavigationNode(P2);

//    Console.WriteLine("Edges from P2:\n");
//    foreach (var edge in p2_edges)
//    {
//        Console.WriteLine($"Id: {edge.Id}\t\tinV: {edge.InVertexId}\t\toutV: {edge.OutVertexId}\t\tdistance: {edge.Distance}");
//    }

//    Console.WriteLine("Try to find path between P1->P3.");
//    IAlghorithmDal AlgDal = new AlghorithmDal(gremlinClient);
//    var pathes = AlgDal.FindAllPathesBetweenVertices(P1, P3);


//    Console.WriteLine("Print edges and vertices in path:\n");
//    foreach (var path in pathes)
//    {
//        Console.WriteLine("Vertices:\n");
//        foreach (var node in path.VertexArray)
//        {
//            Console.WriteLine($"Id: {node.Id}\t\t type: {node.Type}\t\t lable: {node.Lable}");
//        }
//        Console.WriteLine("\nEdges:\n");
//        foreach (var edge in path.EdgeArray)
//        {
//            Console.WriteLine($"Id: {edge.Id}\t\tinV: {edge.InVertexId}\t\toutV: {edge.OutVertexId}");
//        }
//    }

//    var cost_pathes = AlgDal.FindAllPathesWithCostBetweenVertices(P1, P3);

//    Console.WriteLine("Print all weighted pathes cost:\n");
//    foreach (var path in cost_pathes.PathesWithCost)
//    {
//        Console.WriteLine($"Cost: {path.Item1}");
//    }

//    var cost_pathes_nav_room = AlgDal.FindAllPathesWithCostBetweenVertices(P1, Room1);

//    Console.WriteLine("Print all weighted pathes cost from P1 to Room1:\n");
//    foreach (var path in cost_pathes_nav_room.PathesWithCost)
//    {
//        Console.WriteLine($"Cost: {path.Item1}");
//    }


//    var all_rooms = RoomDal.GetAllLectureRoomNodes();

//    Console.WriteLine("Print all lecture rooms:\n");
//    foreach (var room in all_rooms)
//    {
//        Console.WriteLine($"Id: {room.Id}\t\tName: {room.Name}\t\tNumberOfSeats: {room.NumberOfSeats}");
//    }

//}


using (var gremlinClient = new GremlinClient(
                gremlinServer,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType))
{

    FacultyDTO AppliedMath = new FacultyDTO { Id = new Guid().ToString(), Name = "Applied mathematics and informatics" };
    //LectureRoomDTO Room1 = new() { Id = "Room1", Floor = 1, Name = "111", NumberOfSeats = 60, X = 1254.501875, Y = 502.940478 };
    //LectureRoomDTO Room2 = new() { Id = "Room2", Floor = 1, Name = "112", NumberOfSeats = 30, X = 1356.318469, Y = 613.092833 };

    //ILectureRoomNodeDal lectureRoomNodeDal = new LectureRoomDal(gremlinClient);
    //lectureRoomNodeDal.AddLectureRoomNode(Room1);
    //lectureRoomNodeDal.AddLectureRoomNode(Room2);

    IFacultyDal facultyDal = new FacultyDal(gremlinClient);
    facultyDal.AddFaculty(AppliedMath);

    //IFacultyEdgeDal facultyEdgeDal = new FacultyEdgeDal(gremlinClient);
    //facultyEdgeDal.AddLectureRoomToFaculty(Room1, AppliedMath);
    //facultyEdgeDal.AddLectureRoomToFaculty(Room2, AppliedMath);

    //var first = facultyEdgeDal.GetFacultyForLectureRoom(Room1);
    //var second = facultyEdgeDal.GetFacultyForLectureRoom(Room2);




}