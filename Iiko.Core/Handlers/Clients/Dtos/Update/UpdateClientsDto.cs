using Iiko.Handlers.Clients.Dtos.Create;

namespace Iiko.Handlers.Clients.Dtos.Update;

public class UpdateClientsDto
{
    public List<CreateClientDto> Clients { get; set; }
}