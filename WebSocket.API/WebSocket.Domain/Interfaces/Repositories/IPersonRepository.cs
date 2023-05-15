using WebSocket.Domain.Entitys;

namespace WebSocket.Domain.Interfaces.repositories;

public interface IPersonRepository
{
    public Person Create(Person person);
    public Task<Person> Exists(string email);
}