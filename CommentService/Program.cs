using CommentService.CommentBackgroundServices.PostBackgroundProcessor;
using CommentService.EventProcessing.PostProcessors;
using CommentService.Models;
using CommentService.Services.CommentService;
using CommentService.Services.PostService;
using CommentService.Services.PostService.PostService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Get Connection String and register AppDbContext
var conString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDbContext>(options=>options.UseSqlServer(conString));


builder.Services.AddScoped<ICommentService, CommentServices>();
builder.Services.AddScoped<IPostService, PostServices>();

builder.Services.AddSingleton<IAddPostEventProcessor, AddPostEventProcessor>();
builder.Services.AddHostedService<PostMessageSubscriber>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
