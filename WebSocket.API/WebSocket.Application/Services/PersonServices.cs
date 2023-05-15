using WebSocket.Application.utility;
using WebSocket.Domain.Dtos;
using WebSocket.Domain.Entitys;
using WebSocket.Domain.Error;
using WebSocket.Domain.Interfaces;
using WebSocket.Domain.Interfaces.Context;
using WebSocket.Domain.Interfaces.repositories;
using WebSocket.Infra.Authentication;
using WebSocket.Infrastructure.Context;

namespace WebSocket.Application.Services;

public class PersonServices : IPersonServices
{
    private readonly UnitOfWork _uow;
    private readonly IPasswordHash _passwordHash;
    private readonly IPersonRepository _personRep;
    private readonly ICredentialsRepository _credentialsRep;

    public PersonServices
    (
        UnitOfWork uow,
        IPasswordHash passwordHash,
        IPersonRepository personRep,   
        ICredentialsRepository credentialsRep
    )
    {
        _uow = uow;
        _passwordHash = passwordHash;
        _personRep = personRep;
        _credentialsRep = credentialsRep;
    }

    public async Task<RegisterPersonDto> RegisterPerson(RegisterPersonDto person)
    {
        var emailAlready = await _personRep.Exists(person.email);
        if (emailAlready != null)
        {
            throw new ConflictException("[Error] The email entered is already registered.");
        }

        var hash = _passwordHash.generate(person.password);
        var Person = new Person { email = person.email, name = person.name };
        var Credentials = new Credentials
        {
            password_hash = hash.password_hash,
            password_salt = hash.password_salt,
            person = Person,
        };

        _personRep.Create(Person);
        _credentialsRep.Create(Credentials);

        _uow.Commit();
        return person;
    }

    public async Task<IPersonLoginOutput> LoginPerson(PersonLoginDto personLoginDto)
    {
        var email = personLoginDto.email;
        var password = personLoginDto.password;
        var personExists = await _personRep.Exists(email);
        if (personExists is null)
        {
            throw new BadRequestException("[Error] Invalid email or password");
        }

        var matchPassword = _passwordHash.verify(password, personExists.Credentials.password_hash);
        if (matchPassword == false)
        {
            throw new BadRequestException("[Error] Invalid email or password");
        }

        string acess_token = new AuthenticationService().generate(personExists);
        return new IPersonLoginOutput
        {
            acess_token = acess_token,
        };
    }
}