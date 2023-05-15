namespace User.Controllers
{

    using Microsoft.AspNetCore.Mvc;
    using WebSocket.Application.Entity;

    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public async Task<UserEntity> Get()
        {
            return new UserEntity();
        }
    }
}