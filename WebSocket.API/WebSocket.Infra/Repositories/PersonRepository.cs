using Microsoft.EntityFrameworkCore;
using WebSocket.Domain.Entitys;
using WebSocket.Domain.Interfaces.repositories;
using WebSocket.Infrastructure.Context;

namespace WebSocket.Infra.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly DbContextPg _context;

    public PersonRepository(DbContextPg context)
    {
        _context = context;
    }

    public Person Create(Person person)
    {
        _context.Person.Add(person);
        return person;
    }

    public async Task<Person> Exists(string email)
    {
        var person = await _context.Person.Where(person => person.email == email)
            .Include(x => x.Credentials)
            .FirstOrDefaultAsync();
        return person ?? null;
    }
}