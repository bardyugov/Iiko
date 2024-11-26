namespace Iiko.Application.Commands;

public class InsertClientCommand(long clientId, string username)
{
    public long ClientId { get; private set; } = clientId;

    public string Username { get; private set; } = username;
}