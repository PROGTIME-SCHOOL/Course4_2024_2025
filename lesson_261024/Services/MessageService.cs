using System;
using lesson_261024.Services.Interfaces;
namespace lesson_261024.Services;

public class MessageService : IMessageService
{
    public string GetMessage()
    {
        return "Hello from MessageService!";
    }
}
