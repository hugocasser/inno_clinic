using System.Text;

namespace Application.Services.Notifications;

public class Notification
{
    private readonly DateTime _sendTime;
    private readonly string _sender;
    private readonly List<string> _messages  = [];
    private readonly StringBuilder _messageBuilder = new StringBuilder().AppendLine();

    private Notification(string sender)
    {
        _sender = sender;
        _sendTime = DateTime.UtcNow;
    }
    
    public static Notification Create(string sender)
    {
        var notification = new Notification(sender);

        return notification;
    }
    public void AddMessage(string message)
    {
        _messages.Add(message);
    }
    
    public string Build()
    {
        _messageBuilder.AppendLine(_sender);
        _messageBuilder.AppendLine();
        _messageBuilder.AppendLine("Time: " + _sendTime);
        _messageBuilder.AppendLine();
        _messageBuilder.AppendLine("Messages: ");
        foreach (var message in _messages)
        {
            _messageBuilder.AppendLine(message);
        }
        return _messageBuilder.ToString();
    }
}