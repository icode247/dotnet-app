// using Microsoft.AspNetCore.SignalR;
// using System.Threading.Tasks;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddSignalR();

// var app = builder.Build();

// app.MapGet("/", () => "Hello World!");
// app.MapHub<ChatHub>("/chatHub/negotiate");

// app.Run();

// public class ChatHub : Hub
// {
//     public async Task SendMessage(string messageType, string message)
//     {
//         await Clients.All.SendAsync("NewIncomingMessage", messageType, message);
//     }
// }
using SignalRWebpack.Hubs;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Replace with your client's origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();
app.UseCors("CorsPolicy");
app.UseDefaultFiles();
app.UseStaticFiles();
app.MapHub<ChatHub>("/hub");
// app.MapGet("/", () => "Hello World!");

app.Run();