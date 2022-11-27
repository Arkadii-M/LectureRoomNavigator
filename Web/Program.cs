using BLL.Concrete;
using BLL.Interface;
using DAL.Concrete;
using DAL.Interface;
using Gremlin.Net.Driver;
using Gremlin.Net.Structure.IO.GraphSON;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Web.Helpers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
const bool EnableSSL = false; // TODO: check on azure service

string Host = Environment.GetEnvironmentVariable("COSMOS_DB_HOST") ?? throw new ArgumentException("Missing env var: Host");
string PrimaryKey = Environment.GetEnvironmentVariable("COSMOS_DB_PRIMARY_KEY") ?? throw new ArgumentException("Missing env var: Primary key");
string Database = Environment.GetEnvironmentVariable("COSMOS_DB_DATABASE_NAME") ?? throw new ArgumentException("Missing env var: Database name");
string Container = Environment.GetEnvironmentVariable("COSMOS_DB_CONTAINER_NAME") ?? throw new ArgumentException("Missing env var: Container name");
int Port = Convert.ToInt32(Environment.GetEnvironmentVariable("COSMOS_DB_PORT") ?? throw new ArgumentException("Missing env var: Port"));
//int Port = 8081;
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
builder.Services.AddSingleton<IFacultyDal, FacultyDal>();
builder.Services.AddSingleton<IFacultyEdgeDal, FacultyEdgeDal>();
builder.Services.AddSingleton<IRoleDal, RoleDal>();
builder.Services.AddSingleton<IRoleEdgeDal, RoleEdgeDal>();
builder.Services.AddSingleton<IUserDal, UserDal>();


builder.Services.AddSingleton<INavigationManager, NavigationManager>();
builder.Services.AddSingleton<ILectrueRoomManger, LectureRoomManger>();
builder.Services.AddSingleton<IPathManager, PathManager>();
builder.Services.AddSingleton<IFacultyManager, FacultyManager>();
builder.Services.AddSingleton<IUserManager, UserManager>();


builder.Services.AddControllersWithViews();

builder.Services.AddEndpointsApiExplorer(); // For swagger
builder.Services.AddSwaggerGen();

// Add logger
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add options for JWT
builder.Services.AddAuthorization();
//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidIssuer = Environment.GetEnvironmentVariable("JWT_AUTH_ISSUER") ?? throw new ArgumentException("Missing env var: AUTH_ISSUER"),

//            ValidateAudience = true,
//            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUTH_AUDIENCE") ?? throw new ArgumentException("Missing env var: AUTH_AUDIENCE"),

//            ValidateLifetime = true,
//            IssuerSigningKey = new SymmetricSecurityKey(
//                Encoding.UTF8.GetBytes(
//                    Environment.GetEnvironmentVariable("JWT_AUTH_KEY") ?? throw new ArgumentException("Missing env var: AUTH_AUDIENCE")
//                    )),
//            ValidateIssuerSigningKey = true,
//        };
//    });
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer =AuthHelper.Issuer,

            ValidateAudience = true,
            ValidAudience = AuthHelper.Audience,

            ValidateLifetime = true,
            IssuerSigningKey = AuthHelper.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });
var app = builder.Build();

app.UseSwagger();//For swagger
app.UseSwaggerUI();//For swagger



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
//
app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
