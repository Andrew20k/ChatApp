using System.Data;
using System;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ChatApp.Models;

public class ChatMessage
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserName { get; set; }
    public string Message { get; set; }
    public DateTime CreatedOn { get; set; } = TimeZoneInfo.ConvertTime(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
    public string FormattedCreatedOn => CreatedOn.ToString("yyyy-MM-dd, HH:mm:ss");
}

