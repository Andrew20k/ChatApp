using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatApp.Models;

public interface IDatabaseManager
{
    Task SaveChatMessage(ChatMessage message);
    Task<List<ChatMessage>> GetChatHistory();
}