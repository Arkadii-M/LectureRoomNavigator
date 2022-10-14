using BLL.Concrete;
using BLL.Interface;
using DAL.Concrete;
using DAL.Interface;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const bool EnableSSL = false; // TODO: check on azure service

string Host = Environment.GetEnvironmentVariable("COSMOS_DB_HOST") ?? throw new ArgumentException("Missing env var: Host");
string PrimaryKey = Environment.GetEnvironmentVariable("COSMOS_DB_PRIMARY_KEY") ?? throw new ArgumentException("Missing env var: Primary key");
string Database = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME") ?? throw new ArgumentException("Missing env var: Database name");
string Container = Environment.GetEnvironmentVariable("COSMOS_DB_CONTAINER_NAME") ?? throw new ArgumentException("Missing env var: Container name");
int Port = Convert.ToInt32(Environment.GetEnvironmentVariable("COSMOS_DB_PORT") ?? throw new ArgumentException("Missing env var: Port"));

string containerLink = "/dbs/" + Database + "/colls/" + Container;

var gremlinServer = new GremlinServer(Host, Port, enableSsl: EnableSSL,
                                        username: containerLink,
                                        password: PrimaryKey);


var gremlinClient = new GremlinClient(
                gremlinServer,
                new GraphSON2Reader(),
                new GraphSON2Writer(),
                GremlinClient.GraphSON2MimeType);

builder.Services.AddSingleton<IGremlinClient>(gremlinClient);
builder.Services.AddSingleton<IAlghorithmDal, AlghorithmDal>();
builder.Services.AddSingleton<ILectureRoomNodeDal, LectureRoomDal>();
builder.Services.AddSingleton<INavigationNodeDal, NavigationNodeDal>();
builder.Services.AddSingleton<INavigationEdgeDal, NavigationEdgeDal>();

builder.Services.AddSingleton<INavigationManager, NavigationManager>();
builder.Services.AddSingleton<ILectrueRoomManger, LectureRoomManger>();
builder.Services.AddSingleton<IPathManager, PathManager>();


builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}


app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
