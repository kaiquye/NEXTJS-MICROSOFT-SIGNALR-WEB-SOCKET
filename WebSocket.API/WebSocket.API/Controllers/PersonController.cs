using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebSocket.Domain.Dtos;
using WebSocket.Domain.Error;
using WebSocket.Domain.Interfaces;

namespace WebSocket.Api.Controller;


[ApiController]
[Route("/api/v1/person")]
public class PersonController
{
    private readonly IPersonServices _personServices;

    public PersonController(IPersonServices personServices)
    {
        _personServices = personServices;
    }

    [HttpPost]
    public async Task<IResult> create(RegisterPersonDto person)
    {
        return Results.Ok(await _personServices.RegisterPerson(person));
    }
    
    [HttpPost("/login")]
    public async Task<IResult> login(PersonLoginDto person)
    {
        return Results.Ok(await _personServices.LoginPerson(person));
    }
}