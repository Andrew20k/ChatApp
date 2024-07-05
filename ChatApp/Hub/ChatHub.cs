using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Hub;

public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
{
    private readonly IDatabaseManager _databaseManager;

    public ChatHub(IDatabaseManager databaseManager)
    {
        _databaseManager = databaseManager;
    }
    
    public const string ReceiveMessage = "ReceiveMessage";
    
    public async Task SendMessage(ChatMessage msg)
    {
        if (string.IsNullOrEmpty(msg.Id))
        {
            msg.Id = Guid.NewGuid().ToString();
        }

        // Ensure UserName is not null or empty
        if (string.IsNullOrEmpty(msg.UserName))
        {
            throw new ArgumentException("UserName is required");
        }
    
        await _databaseManager.SaveChatMessage(msg);
        await Clients.All.SendAsync(ReceiveMessage, msg);
    }

    public async Task GetChatHistory()
    {
        var messages = await _databaseManager.GetChatHistory();
        await Clients.Caller.SendAsync("ReceiveMessageHistory", messages);
    }

}