using System.ComponentModel.DataAnnotations.Schema;
using WebSocket.Domain.Entitys.Interface;

namespace WebSocket.Domain.Entitys;

public class Credentials: EntityBase
{
    public string password_salt { get; set; }
    public string password_hash { get; set; }
    public Guid person_id { get; set; }
    public Person person { get; set; }
}