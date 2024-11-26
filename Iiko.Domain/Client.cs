namespace Iiko.Domain;

public class Client(long clientId, string username, Guid systemId)
{
    public long ClientId { get; set; } = clientId;
    
    public string Username { get; set; } = username;

    public Guid SystemId { get; set; } = systemId;
    
}