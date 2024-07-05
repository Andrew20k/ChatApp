using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp.Models;

public class DatabaseManager : IDatabaseManager
{
    private readonly ChatContext _context;

    public DatabaseManager(ChatContext context)
    {
        _context = context;
    }

    public async Task SaveChatMessage(ChatMessage message)
    {
        _context.ChatMessages.Add(message);
        await _context.SaveChangesAsync();
    }

    public async Task<List<ChatMessage>> GetChatHistory()
    {
        return await _context.ChatMessages.OrderBy(m => m.CreatedOn).ToListAsync();
    }
}