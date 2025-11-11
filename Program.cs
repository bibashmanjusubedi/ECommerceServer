using ecomServer.DAL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ecomServerDbContext>(options => 
    options.UseNpgsql("Host=aws-1-ap-southeast-2.pooler.supabase.com;Port=5432;Database=postgres;Username=postgres.blikyxhbwtuoxqzcltip;Password=Ecommercesupabase@#123;SSL Mode=Require;Pooling=true"))


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
