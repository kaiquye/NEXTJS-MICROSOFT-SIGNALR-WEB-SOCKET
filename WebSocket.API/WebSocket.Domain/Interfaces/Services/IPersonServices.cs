using WebSocket.Domain.Dtos;

namespace WebSocket.Domain.Interfaces;

public struct IPersonLoginOutput
{
    public string acess_token { get; set; }
}

public interface IPersonServices
{
    public Task<RegisterPersonDto> RegisterPerson(RegisterPersonDto person);
    public Task<IPersonLoginOutput> LoginPerson(PersonLoginDto personLoginDto);
}