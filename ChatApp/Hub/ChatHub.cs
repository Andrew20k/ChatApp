using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
    public const string ReceiveMessage = "ReceiveMessage";
    
    public async Task SendMessage(ChatMessage msg)
    {
          // TODO: save msg to database
          await Clients.All.SendAsync(ReceiveMessage, msg);
    }
}