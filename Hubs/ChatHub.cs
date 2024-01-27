using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SignalRWebpack.Hubs
{
    public class ChatMessage
    {
        public required string Username { get; set; }
        public required string Message { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class ChatHub : Hub
    {
        private static readonly List<ChatMessage> Messages = new List<ChatMessage>();

        public async Task NewMessage(string message)
        {
            var username = Context.ConnectionId; // Correctly accessing Context within the Hub class
            var chatMessage = new ChatMessage
            {
                Username = username,
                Message = message,
                Timestamp = DateTime.UtcNow
            };

            Messages.Add(chatMessage); // Store message in memory
            // await Clients.All.SendAsync("messageReceived", username, message, chatMessage.Timestamp);
                await Clients.Others.SendAsync("messageReceived", username, message, chatMessage.Timestamp);

        }

        public async Task GetAllMessages() => 
            await Clients.Caller.SendAsync("allMessages", Messages);
    }
}