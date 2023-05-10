using Microsoft.EntityFrameworkCore;
using PostService.Models;
using PostService.ServiceBus;
using PostService.Services.PostService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Get ConnectionString 
var conString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(conString));

// Register Post Service
builder.Services.AddScoped<IPostService, PostServices>();

// Register Service bus 
builder.Services.AddSingleton<IPostServiceBus, PostServiceBus>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
