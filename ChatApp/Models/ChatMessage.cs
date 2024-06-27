using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatApp.Models;

public class ChatMessage
{

    public ChatMessage()
    {
        CreatedOn = DateTime.Now;
    }
    
    public string UserName { get; set; }
    public string Message { get; set; }
    
    public DateTime CreatedOn { get; }
    public string FormattedCreatedOn => CreatedOn.ToString( "yyyy-MM-dd, HH:mm:ss");
}