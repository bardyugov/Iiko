using Aiko.Handlers.Clients.Dtos.Create;

namespace Aiko.Handlers.Clients.Dtos.Update;

public class UpdateClientsDto
{
    public List<CreateClientDto> Clients { get; set; }
}