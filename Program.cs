using ecomServer.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ecomServerDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")))


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
