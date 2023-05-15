using WebSocket.Domain.Entitys.Interface;

namespace WebSocket.Domain.Entitys;

public class Person: EntityBase
{
    public string name { get; set; }
    public string email { get; set; }
    public Credentials Credentials { get; set; }
}